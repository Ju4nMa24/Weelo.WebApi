using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Authentication;
using Weelo.Abstrations.Types.Owners;
using Weelo.Abstrations.Types.Properties;
using Weelo.Business.Commands.Authentication;
using Weelo.Business.Commands.Owner;
using Weelo.Business.Commands.Property;
using Weelo.Business.Proccesors.Authentication;
using Weelo.Business.Proccesors.Owners;
using Weelo.Business.Proccesors.Properties;
using Weelo.Common.Types.Authentication;
using Weelo.Common.Types.Owners;
using Weelo.Common.Types.Properties;
using Weelo.Repository.SqlServer.DataContext;
using Weelo.Repository.SqlServer.Services;
using Weelo.WebApi.Mappers;

namespace Weelo.WebApi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region ENTITIES AND REPOSITORIES
            services.AddScoped<IAuth, Auth>();
            services.AddSingleton<IOwner, Owner>();
            services.AddSingleton<IProperty, Property>();
            services.AddSingleton<IPropertyImage, PropertyImage>();
            services.AddSingleton<IPropertyTrace, PropertyTrace>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());
            #endregion
            #region RESPONSE
            services.AddScoped<IRequestHandler<OwnerCommand, OwnerResponse>, OwnerProccesor>();
            services.AddScoped<IRequestHandler<PropertyCommand, PropertyResponse>, PropertyProccesor>();
            services.AddScoped<IRequestHandler<OwnerConsultCommand, OwnerConsultResponse>, OwnerConsultProccesor>();
            services.AddScoped<IRequestHandler<PricePropertyCommand, PricePropertyResponse>, PropertyPriceProccesor>();
            services.AddScoped<IRequestHandler<AuthenticationCommand, AuthenticationResponse>, AuthenticationProccesor>();
            services.AddScoped<IRequestHandler<PropertyFiltersCommand, PropertyFiltersResponse>, PorpertyFilterProccesor>();
            services.AddScoped<IRequestHandler<PropertyAddImageCommand, PropertyAddImageResponse>, PropertyAddImageProccesor>();
            services.AddScoped<IRequestHandler<PropertyPurchaseCommand, PropertyPurchaseResponse>, PropertyPurchaseProccesor>();
            #endregion
            #region CONNECTION LOGS
            services.AddDbContext<WeeloContext>(options => options.UseSqlServer(_configuration["connectionString"]));
            #endregion
            #region NUGGETS INSTANCIATE
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            #region LANGUAGE
            CultureInfo[] supportedCultures = new[]
            {
                    new CultureInfo("es"),
                    new CultureInfo("en")
            };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("es");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            #endregion
            #region Json Web Token Configuration
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtParameters:SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtParameters:Audience"],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion
            //Cors Policy Implementation.
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .WithOrigins(_configuration["Security:CorsPolicy:AllowedOrigins"].Split(","));
            }));
            //HSTS Implementation.
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("google.com");
                options.ExcludedHosts.Add("www.example.com");
            });
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
            else
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                    options.HttpsPort = 5001;
                });
            }
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Definitions", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Exception Handler implementation.
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    IExceptionHandlerPathFeature exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json;charset=" + Encoding.UTF8.WebName;
                    await context.Response.WriteAsync("{\"codigo\": -1, \"respuesta\" : \"Internal Error\"}");
                });
            });
            //CSP Implementation.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("Header-Name", "Header-Value");
                Activity activity = new Activity("WebApi").Start();
                await next();
                activity.Stop();
            });
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weelo.WebApi v1"));
            }

            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                Activity activity = new Activity("Weelo.WebApi");
                activity.SetIdFormat(ActivityIdFormat.W3C);
                await next();

                activity.Stop();
            });
            app.UseRequestLocalization();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

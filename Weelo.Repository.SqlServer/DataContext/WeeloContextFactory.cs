using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Weelo.Repository.SqlServer.DataContext
{
    /// <summary>
    /// Context Factory Db.
    /// </summary>
    public class WeeloContextFactory : IDesignTimeDbContextFactory<WeeloContext>
    {
        /// <summary>
        /// Create the db contexty.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public WeeloContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../../Weelo.WebApi/Weelo.WebApi/appsettings.json")).Build();
            DbContextOptionsBuilder<WeeloContext> optionBuilder = new();
            optionBuilder.UseSqlServer(configuration["connectionString"]);
            return new WeeloContext(optionBuilder.Options);
        }
    }
}

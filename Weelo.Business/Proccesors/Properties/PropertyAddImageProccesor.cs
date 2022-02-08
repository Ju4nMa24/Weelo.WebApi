using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Business.Commands.Property;
using Weelo.Common.Generics;
using Weelo.Common.Types.Properties;

namespace Weelo.Business.Proccesors.Properties
{
    /// <summary>
    /// Business logic for the property image process.
    /// </summary>
    public class PropertyAddImageProccesor : IRequestHandler<PropertyAddImageCommand, PropertyAddImageResponse>
    {
        #region INSTANTIATE
        private readonly PropertyAddImageResponse _propertyAddImageResponse;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly ILogger<PropertyAddImageProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion
        public PropertyAddImageProccesor(IPropertyRepository propertyRepository, ILogger<PropertyAddImageProccesor> logger, IMapper mapper, IOwnerRepository ownerRepository, IPropertyImageRepository propertyImageRepository)
        {
            _propertyAddImageResponse = new();
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PropertyAddImageResponse> Handle(PropertyAddImageCommand request, CancellationToken cancellationToken)
        {
            _propertyAddImageResponse.InnerContext.Result.Success = false;
            try
            {
                //Validation of input parameters.
                if (!(new PropertyAddImageValidator()).Validate(request).IsValid)
                {
                    _propertyAddImageResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _propertyAddImageResponse
                    }).InnerContext;
                    _propertyAddImageResponse.InnerContext.Result.Details = (new PropertyAddImageValidator()).Validate(request).Errors.ToArray();
                    _propertyAddImageResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_propertyAddImageResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _propertyAddImageResponse;
                }
                //The submitted property is searched.
                string idProperty = await _propertyRepository.Find(request.CodeInternal);
                if (string.IsNullOrEmpty(idProperty))
                {
                    _propertyAddImageResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _propertyAddImageResponse
                    }).InnerContext;
                    _propertyAddImageResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_propertyAddImageResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _propertyAddImageResponse;
                }
                //Images are added.
                Parallel.ForEach(request.Files, file =>
                {
                    _propertyImageRepository.Create(new PropertyImage()
                    {
                        Enabled = file.Enabled,
                        File = file.FileUrl,
                        IdPropertyImage = Guid.NewGuid(),
                        IdProperty = Guid.Parse(idProperty)
                    });
                });
                if (!string.IsNullOrEmpty(idProperty))
                {
                    _propertyAddImageResponse.InnerContext.Result.Success = true;
                    _propertyAddImageResponse.InnerContext = Resource.SuccessMessage(new()
                    {
                        Header = Constants.SuccessMessage,
                        Parameter = Constants.Code00,
                        ResponseType = _propertyAddImageResponse
                    }).InnerContext;
                    _propertyAddImageResponse.StatusCode = HttpStatusCode.OK.ToString();
                    string detail = JsonConvert.SerializeObject(_propertyAddImageResponse.InnerContext);
                    _logger.LogInformation(detail);
                    return _propertyAddImageResponse;
                }
            }
            catch (Exception ex)
            {
                _propertyAddImageResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _propertyAddImageResponse
                }).InnerContext;
                _propertyAddImageResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _propertyAddImageResponse;
            }
            return _propertyAddImageResponse;
        }
    }
}

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
    /// Business logic for the property creation process.
    /// </summary>
    public class PropertyProccesor : IRequestHandler<PropertyCommand, PropertyResponse>
    {
        #region INSTANTIATE
        private readonly PropertyResponse _propertyResponse;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILogger<PropertyProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion

        public PropertyProccesor(IPropertyRepository propertyRepository, ILogger<PropertyProccesor> logger, IMapper mapper, IOwnerRepository ownerRepository)
        {
            _propertyResponse = new();
            _propertyRepository = propertyRepository;
            _ownerRepository = ownerRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PropertyResponse> Handle(PropertyCommand request, CancellationToken cancellationToken)
        {
            _propertyResponse.InnerContext.Result.Success = false;
            try
            {
                //Validation of input parameters.
                if (!(new PropertyValidator()).Validate(request).IsValid)
                {
                    _propertyResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _propertyResponse
                    }).InnerContext;
                    _propertyResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _propertyResponse;
                }
                //If the internal code is sent, the property update process is carried out.
                if (!string.IsNullOrEmpty(request.CodeInternalOrigin))
                {
                    //Property Update
                    Guid property = _propertyRepository.Update(_mapper.Map<Property>(request), request.CodeInternalOrigin);
                    if (!string.IsNullOrEmpty(property.ToString()))
                    {
                        _propertyResponse.InnerContext.Result.Success = true;
                        _propertyResponse.IdProperty = property.ToString();
                        _propertyResponse.InnerContext = Resource.SuccessMessage(new()
                        {
                            Header = Constants.SuccessMessage,
                            Parameter = Constants.Code00,
                            ResponseType = _propertyResponse
                        }).InnerContext;
                        _propertyResponse.StatusCode = HttpStatusCode.OK.ToString();
                        string detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _propertyResponse;
                    }
                    else
                    {
                        _propertyResponse.InnerContext = Resource.ErrorResponse(new()
                        {
                            Header = Constants.HeaderErrorMessage,
                            Parameter = Constants.Code99,
                            ResponseType = _propertyResponse
                        }).InnerContext;
                        _propertyResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                        string detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                        _logger.LogWarning(detail);
                        return _propertyResponse;
                    }
                }
                //Property creation.
                if (!(new PropertyValidatorCreate()).Validate(request).IsValid)
                {
                    _propertyResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _propertyResponse
                    }).InnerContext;
                    _propertyResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _propertyResponse;
                }
                Guid idOwner = _ownerRepository.Find(request.IdentificationNumner).Result.IdOwner;
                request.IdOwner = idOwner;
                string idProperty = await _propertyRepository.Create(_mapper.Map<Property>(request));
                if (!string.IsNullOrEmpty(idProperty.ToString()))
                {
                    _propertyResponse.InnerContext.Result.Success = true;
                    _propertyResponse.IdProperty = idProperty.ToString();
                    _propertyResponse.InnerContext = Resource.SuccessMessage(new()
                    {
                        Header = Constants.SuccessMessage,
                        Parameter = Constants.Code00,
                        ResponseType = _propertyResponse
                    }).InnerContext;
                    _propertyResponse.StatusCode = HttpStatusCode.OK.ToString();
                    string detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                    _logger.LogInformation(detail);
                    return _propertyResponse;
                }
            }
            catch (Exception ex)
            {
                _propertyResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _propertyResponse
                }).InnerContext;
                    _propertyResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _propertyResponse;
            }
            return _propertyResponse;
        }

    }
}

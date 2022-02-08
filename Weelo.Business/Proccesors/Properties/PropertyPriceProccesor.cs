using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Properties;
using Weelo.Business.Commands.Property;
using Weelo.Common.Generics;
using Weelo.Common.Types.Properties;

namespace Weelo.Business.Proccesors.Properties
{
    /// <summary>
    /// Business logic for the property price process.
    /// </summary>
    public class PropertyPriceProccesor : IRequestHandler<PricePropertyCommand, PricePropertyResponse>
    {
        #region INSTANTIATE
        private readonly PricePropertyResponse _propertyResponse;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ILogger<PropertyPriceProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion

        public PropertyPriceProccesor(IPropertyRepository propertyRepository, ILogger<PropertyPriceProccesor> logger, IMapper mapper, IOwnerRepository ownerRepository)
        {
            _propertyResponse = new();
            _propertyRepository = propertyRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PricePropertyResponse> Handle(PricePropertyCommand request, CancellationToken cancellationToken)
        {
            _propertyResponse.InnerContext.Result.Success = false;
            try
            {
                string detail = String.Empty;
                //Validation of input parameters.
                if (!(new PricePropertyValidator()).Validate(request).IsValid)
                {
                    _propertyResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _propertyResponse
                    }).InnerContext;
                    _propertyResponse.InnerContext.Result.Details = (new PricePropertyValidator()).Validate(request).Errors.ToArray();
                    _propertyResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _propertyResponse;
                }
                //Price update
                IProperty property = await _propertyRepository.PriceUpdate(request.Price,request.CodeInternal);
                if (property is not null)
                {
                    _propertyResponse.InnerContext.Result.Success = true;
                    _propertyResponse.Property = property;
                    _propertyResponse.InnerContext = Resource.SuccessMessage(new()
                    {
                        Header = Constants.SuccessMessage,
                        Parameter = Constants.Code00,
                        ResponseType = _propertyResponse
                    }).InnerContext;
                    _propertyResponse.StatusCode = HttpStatusCode.OK.ToString();
                    detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
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
                    detail = JsonConvert.SerializeObject(_propertyResponse.InnerContext);
                    _logger.LogWarning(detail);
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
        }
    }
}

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
    public class PropertyPurchaseProccesor : IRequestHandler<PropertyPurchaseCommand, PropertyPurchaseResponse>
    {
        #region INSTANTIATE
        private readonly PropertyPurchaseResponse _propertyResponse;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ILogger<PropertyPurchaseProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion

        public PropertyPurchaseProccesor(IPropertyRepository propertyRepository, ILogger<PropertyPurchaseProccesor> logger, IMapper mapper, IOwnerRepository ownerRepository)
        {
            _propertyResponse = new();
            _propertyRepository = propertyRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PropertyPurchaseResponse> Handle(PropertyPurchaseCommand request, CancellationToken cancellationToken)
        {
            _propertyResponse.InnerContext.Result.Success = false;
            try
            {
                //Validation of input parameters.
                if (!(new PropertyPurchaseValidator()).Validate(request).IsValid)
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
                if (await _propertyRepository.PurchaseRecord(_mapper.Map<PropertyTrace>(request), request.CodeInternal))
                {
                    _propertyResponse.InnerContext.Result.Success = true;
                    _propertyResponse.StatusCode = HttpStatusCode.OK.ToString();
                    _propertyResponse.InnerContext = Resource.SuccessMessage(new()
                    {
                        Header = Constants.SuccessMessage,
                        Parameter = Constants.Code00,
                        ResponseType = _propertyResponse
                    }).InnerContext;
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

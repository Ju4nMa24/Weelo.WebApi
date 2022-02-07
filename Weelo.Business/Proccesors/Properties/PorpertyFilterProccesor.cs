using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PorpertyFilterProccesor : IRequestHandler<PropertyFiltersCommand, PropertyFiltersResponse>
    {
        #region INSTANTIATE
        private readonly PropertyFiltersResponse _propertyResponse;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ILogger<PorpertyFilterProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion

        public PorpertyFilterProccesor(IPropertyRepository propertyRepository, ILogger<PorpertyFilterProccesor> logger, IMapper mapper, IOwnerRepository ownerRepository)
        {
            _propertyResponse = new();
            _propertyRepository = propertyRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PropertyFiltersResponse> Handle(PropertyFiltersCommand request, CancellationToken cancellationToken)
        {
            _propertyResponse.InnerContext.Result.Success = false;
            try
            {
                IEnumerable<IProperty> properties = await _propertyRepository.GetAll(_mapper.Map<Property>(request));
                if (properties.Count() > 0)
                {
                    _propertyResponse.InnerContext.Result.Success = true;
                    _propertyResponse.Properties = properties;
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

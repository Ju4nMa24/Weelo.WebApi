using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Business.Commands.Owner;
using Weelo.Common.Generics;

namespace Weelo.Business.Proccesors.Owners
{
    public class OwnerConsultProccesor : IRequestHandler<OwnerConsultCommand, OwnerConsultResponse>
    {
        #region INSTANTIATE
        private readonly OwnerConsultResponse _ownerResponse;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILogger<OwnerProccesor> _logger;
        private readonly IMapper _mapper;

        public OwnerConsultProccesor(IOwnerRepository ownerRepository, ILogger<OwnerProccesor> logger, IMapper mapper)
        {
            _ownerResponse = new();
            _ownerRepository = ownerRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion
        public async Task<OwnerConsultResponse> Handle(OwnerConsultCommand request, CancellationToken cancellationToken)
        {
            _ownerResponse.InnerContext.Result.Success = false;
            try
            {
                string detail = string.Empty;
                //Validation of input parameters.
                if (!(new OwnerConsultValidator()).Validate(request).IsValid)
                {
                    _ownerResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _ownerResponse
                    }).InnerContext;
                    _ownerResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _ownerResponse;
                }
                dynamic response = await _ownerRepository.Find(request.IdentificationNumber);
                if (response is null)
                {
                    _ownerResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code02,
                        ResponseType = _ownerResponse
                    }).InnerContext;
                    _ownerResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _ownerResponse;
                }
                _ownerResponse.Owner = response;
                _ownerResponse.InnerContext.Result.Success = true;
                _ownerResponse.InnerContext = Resource.SuccessMessage(new()
                {
                    Header = Constants.SuccessMessage,
                    Parameter = Constants.Code00,
                    ResponseType = _ownerResponse
                }).InnerContext;
                    _ownerResponse.StatusCode = HttpStatusCode.OK.ToString();
                detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                _logger.LogWarning(detail);
                return _ownerResponse;
            }
            catch (System.Exception ex)
            {
                _ownerResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _ownerResponse
                }).InnerContext;
                    _ownerResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _ownerResponse;
            }
        }
    }
}

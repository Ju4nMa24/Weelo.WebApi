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
using Weelo.Common.Types.Owners;

namespace Weelo.Business.Proccesors.Owners
{
    public class OwnerProccesor : IRequestHandler<OwnerCommand, OwnerResponse>
    {
        #region INSTANTIATE
        private readonly OwnerResponse _ownerResponse;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILogger<OwnerProccesor> _logger;
        private readonly IMapper _mapper;

        public OwnerProccesor(IOwnerRepository ownerRepository, ILogger<OwnerProccesor> logger, IMapper mapper)
        {
            _ownerResponse = new();
            _ownerRepository = ownerRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion
        public async Task<OwnerResponse> Handle(OwnerCommand request, CancellationToken cancellationToken)
        {
            _ownerResponse.InnerContext.Result.Success = false;
            try
            {
                //Validation of input parameters.
                if (!(new OwnerValidator()).Validate(request).IsValid)
                {
                    _ownerResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _ownerResponse
                    }).InnerContext;
                    _ownerResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _ownerResponse;
                }
                if (!_ownerRepository.GetOne(request.IdentificationNumber))
                {
                    bool response = await _ownerRepository.Create(_mapper.Map<Owner>(request));
                    if (response)
                    {
                        _ownerResponse.InnerContext.Result.Success = true;
                        _ownerResponse.InnerContext = Resource.SuccessMessage(new()
                        {
                            Header = Constants.SuccessMessage,
                            Parameter = Constants.Code00,
                            ResponseType = _ownerResponse
                        }).InnerContext;
                        _ownerResponse.StatusCode = HttpStatusCode.OK.ToString();
                        string detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _ownerResponse;
                    }
                    else
                    {
                        _ownerResponse.InnerContext = Resource.ErrorResponse(new()
                        {
                            Header = Constants.HeaderErrorMessage,
                            Parameter = Constants.Code02,
                            ResponseType = _ownerResponse
                        }).InnerContext;
                        _ownerResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                        string detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                        _logger.LogWarning(detail);
                        return _ownerResponse;
                    }
                }
                else
                {
                    _ownerResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code02,
                        ResponseType = _ownerResponse
                    }).InnerContext;
                    _ownerResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_ownerResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _ownerResponse;
                }

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
            return _ownerResponse;
        }
    }
}

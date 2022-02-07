using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Owners;
using Weelo.Business.Commands.Authentication;
using Weelo.Common.Generics;
using Weelo.Common.Types.Authentication;
using Weelo.Common.Types.Owners;

namespace Weelo.Business.Proccesors.Authentication
{
    public class AuthenticationProccesor : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        #region INSTANTIATE
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILogger<AuthenticationProccesor> _logger;
        private readonly AuthenticationResponse _authenticationResponse;
        private readonly IAuthenticationRepository _authenticationRepository;
        #endregion
        public AuthenticationProccesor(IAuthenticationRepository authenticationRepository, ILogger<AuthenticationProccesor> logger, IMapper mapper, IOwnerRepository ownerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationResponse = new();
            _ownerRepository = ownerRepository;
            _authenticationRepository = authenticationRepository;
        }
        public async Task<AuthenticationResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            _authenticationResponse.InnerContext.Result.Success = false;
            try
            {
                //Validation of input parameters.
                if (!(new AuthenticationValidator()).Validate(request).IsValid)
                {
                    _authenticationResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _authenticationResponse
                    }).InnerContext;
                    _authenticationResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_authenticationResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _authenticationResponse;
                }
                IOwner owner = await _ownerRepository.Find(request.IdentificationNumber);
                if (owner is null && Convert.ToDateTime(owner.Birthday) != Convert.ToDateTime(request.BirthDay))
                {
                    _authenticationResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code03,
                        ResponseType = _authenticationResponse
                    }).InnerContext;
                    _authenticationResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_authenticationResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _authenticationResponse;
                }
                string token = await Task.Run(() => _authenticationRepository.Generate(_mapper.Map<Auth>(request)).Result);
                if (!string.IsNullOrEmpty(token))
                {
                    _authenticationResponse.InnerContext.Result.Success = true;
                    _authenticationResponse.Token = token;
                    _authenticationResponse.StatusCode = HttpStatusCode.OK.ToString();
                    _authenticationResponse.InnerContext = Resource.SuccessMessage(new()
                    {
                        Header = Constants.SuccessMessage,
                        Parameter = Constants.Code00,
                        ResponseType = _authenticationResponse
                    }).InnerContext;
                    string detail = JsonConvert.SerializeObject(_authenticationResponse.InnerContext);
                    _logger.LogInformation(detail);
                    return _authenticationResponse;
                }
            }
            catch (Exception ex)
            {
                _authenticationResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _authenticationResponse
                }).InnerContext;
                _authenticationResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _authenticationResponse;
            }
            return _authenticationResponse;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weelo.Business.Commands.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weelo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("GenerateJwt")]
        public async Task<IActionResult> GenerateJwt([FromBody]AuthenticationCommand authentication)
        {
            AuthenticationResponse response = await _mediator.Send(authentication);
            return !response.InnerContext.Result.Success ? BadRequest(response.InnerContext.Result) : Ok(response.Token);
        }
    }
}

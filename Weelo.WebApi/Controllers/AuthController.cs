using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weelo.Business.Commands.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weelo.WebApi.Controllers
{
    /// <summary>
    /// This controller is oriented for the creation of the jwt.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Exposed method for creating of the jwt.
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GenerateJwt")]
        public async Task<IActionResult> GenerateJwt([FromBody]AuthenticationCommand authentication)
        {
            AuthenticationResponse response = await _mediator.Send(authentication);
            return !response.InnerContext.Result.Success ? BadRequest(response.InnerContext.Result) : Ok(response.Token);
        }
    }
}

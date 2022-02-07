using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weelo.Business.Commands.Owner;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weelo.WebApi.Controllers
{
    /// <summary>
    /// This controller contains the definition of the methods associated with the owners.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Exposed method for querying owners.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        // GET api/<PropertiesController>/5
        [HttpGet("{identificationNumber}")]
        public async Task<IActionResult> FindOwner(string identificationNumber)
        {
            OwnerConsultResponse response = await _mediator.Send(new OwnerConsultCommand() { IdentificationNumber = identificationNumber});
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        /// <summary>
        /// Exposed method for creating owners.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        // POST api/<PropertiesController>
        [HttpPost("OwnerRegister")]
        public async Task<IActionResult> OwnerRegister([FromBody]OwnerCommand owner)
        {
            OwnerResponse response = await _mediator.Send(owner);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
    }
}

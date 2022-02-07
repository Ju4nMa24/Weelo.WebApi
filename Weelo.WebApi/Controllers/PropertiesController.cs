using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weelo.Business.Commands.Property;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weelo.WebApi.Controllers
{
    /// <summary>
    /// This controller contains the definition of the methods associated with the properties.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PropertiesController(IMediator mediator) => _mediator = mediator;
        // POST api/<PropertiesController>
        /// <summary>
        /// Exposed method to get the properties
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost("GetAllProperties")]
        public async Task<IActionResult> GetAllProperties([FromBody]PropertyFiltersCommand property)
        {
            PropertyFiltersResponse response = await _mediator.Send(property);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        /// <summary>
        /// Exposed method for creating the properties.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        // POST api/<PropertiesController>
        [HttpPost("PropertyRegister")]
        public async Task<IActionResult> PropertyRegister([FromBody]PropertyCommand property)
        {
            PropertyResponse response = await _mediator.Send(property);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        /// <summary>
        ///  Exposed method for the creation of the purchase of the properties.   
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        // POST api/<PropertiesController>
        [HttpPost("PurchaseRecord")]
        public async Task<IActionResult> PurchaseRecord([FromBody]PropertyPurchaseCommand property)
        {
            PropertyPurchaseResponse response = await _mediator.Send(property);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        /// <summary>
        /// Exposed method to add the images of the properties.
        /// </summary>
        /// <param name="propertyAddImage"></param>
        /// <returns></returns>
        // POST api/<PropertiesController>
        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage([FromBody]PropertyAddImageCommand propertyAddImage)
        {
            PropertyAddImageResponse response = await _mediator.Send(propertyAddImage);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        /// <summary>
        /// Exposed method for updating prices.
        /// </summary>
        /// <param name="codeInternal"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        // POST api/<PropertiesController>
        [HttpPut("PriceUpdate/{codeInternal}")]
        public async Task<IActionResult> PriceUpdate(string codeInternal, string price)
        {
            PricePropertyResponse response = await _mediator.Send(new PricePropertyCommand()
            {
                CodeInternal = codeInternal,
                Price = price
            });
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        /// <summary>
        /// Exposed method for updating properties.
        /// </summary>
        /// <param name="codeInternal"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        // POST api/<PropertiesController>
        [HttpPut("Update/{codeInternal}")]
        public async Task<IActionResult> Update(string codeInternal, [FromBody]PropertyCommand property)
        {
            property.CodeInternalOrigin = codeInternal;
            PropertyResponse response = await _mediator.Send(property);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
    }
}


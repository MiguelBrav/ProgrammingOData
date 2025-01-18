using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportedLocalesController : ODataController
    {
        private IMediator _mediator;

        public SupportedLocalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                AllSupportedLocalesQuery allLocalesQuery = new AllSupportedLocalesQuery();

                IQueryable<SupportedLocale> locales = await _mediator.Send(allLocalesQuery);

                return Ok(locales);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
       
    }
}

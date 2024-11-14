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
    public class PrlanguagesController : ODataController
    {
        private IMediator _mediator;

        public PrlanguagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                AllPrLanguageQuery allLanguagesQuery = new AllPrLanguageQuery();

                IQueryable<PrLanguage> languages = await _mediator.Send(allLanguagesQuery);

                return Ok(languages);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}

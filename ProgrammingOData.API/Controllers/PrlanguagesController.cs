using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
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

        [EnableQuery]
        [HttpGet("by")]

        public async Task<IActionResult> GetById([FromODataUri] int key)
        {
            try
            {
                ByIdPrLanguageQuery prLanguageQuery = new ByIdPrLanguageQuery
                {
                    Id = key
                };

                SingleResult<PrLanguage> language = await _mediator.Send(prLanguageQuery);

                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}

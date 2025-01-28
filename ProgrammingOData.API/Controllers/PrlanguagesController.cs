using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Helpers;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.DTOS;
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
        public async Task<IActionResult> Get([FromQuery] string? locale)
        {
            try
            {
                AllPrLanguageQuery allLanguagesQuery = new AllPrLanguageQuery
                {
                    Locale = locale
                };

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

        public async Task<IActionResult> GetById([FromODataUri] int key, [FromQuery] string? locale)
        {
            try
            {
                ByIdPrLanguageQuery prLanguageQuery = new ByIdPrLanguageQuery
                {
                    Id = key,
                    Locale = locale
                };

                SingleResult<PrLanguage> language = await _mediator.Send(prLanguageQuery);

                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> CreateLanguage(CreatePrLanguageDTO createLanguage)
        {
            try
            {
                CreatePrLanguageCommand createPrLanguageCommand = new CreatePrLanguageCommand
                {
                    createLanguage = createLanguage
                };

                return await _mediator.Send(createPrLanguageCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);          
            }
        }

        [HttpPut]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> UpdateLanguage(UpdatePrLanguageDTO updateLanguage)
        {
            try
            {
                UpdatePrLanguageCommand updatePrLanguageCommand = new UpdatePrLanguageCommand
                {
                    updateLanguage = updateLanguage
                };

                return await _mediator.Send(updatePrLanguageCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpDelete]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> DeleteLanguage([FromODataUri] int key)
        {
            try
            {
                DeletePrLanguageCommand deletePrLanguageCommand = new DeletePrLanguageCommand
                {
                    deleteLanguage = new DeleteByIdDTO { Id = key } 
                };

                return await _mediator.Send(deletePrLanguageCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}

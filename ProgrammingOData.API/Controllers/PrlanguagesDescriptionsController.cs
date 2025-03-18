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
    public class PrlanguagesDescriptionsController : ODataController
    {
        private IMediator _mediator;

        public PrlanguagesDescriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                AllPrLanguageDescriptionsQuery allLanguagesDescriptionsQuery = new AllPrLanguageDescriptionsQuery();

                IQueryable<PrLanguageDescription> languagesDescriptions = await _mediator.Send(allLanguagesDescriptionsQuery);

                return Ok(languagesDescriptions);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        [EnableQuery]
        [HttpGet("by")]

        public async Task<IActionResult> GetById([FromODataUri] int key)
        {
            try
            {
                ByIdPrLanguageDescriptionQuery prLanguageDescQuery = new ByIdPrLanguageDescriptionQuery
                {
                    Id = key
                };

                SingleResult<PrLanguageDescription> language = await _mediator.Send(prLanguageDescQuery);

                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        [HttpPost]
        public async Task<IActionResult> CreateLanguageDescription(CreatePrLanguageDescDTO createLanguageDescription)
        {
            try
            {
                CreatePrLanguageDescCommand createPrLanguageDescCommand = new CreatePrLanguageDescCommand
                {
                    createLanguageDesc = createLanguageDescription
                };

                return await _mediator.Send(createPrLanguageDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

    }
}

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
    public class PrFrameworksController : ODataController
    {
        private IMediator _mediator;

        public PrFrameworksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? locale)
        {
            try
            {
                AllPrFrameworkQuery allPrFrameworkQuery = new AllPrFrameworkQuery
                {
                    Locale = locale
                };

                IQueryable<PrFramework> frameworks = await _mediator.Send(allPrFrameworkQuery);

                return Ok(frameworks);
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
                ByIdPrFrameworkQuery prFrameworkQuery = new ByIdPrFrameworkQuery
                {
                    Id = key,
                    Locale = locale
                };

                SingleResult<PrFramework> framework = await _mediator.Send(prFrameworkQuery);

                return Ok(framework);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        [HttpPost]
        public async Task<IActionResult> CreateFramework(CreatePrFrameworkDTO createPrFrameworkDTO)
        {
            try
            {
                CreatePrFrameWorkCommand createPrFrameWorkCommand = new CreatePrFrameWorkCommand
                {
                    createFramework = createPrFrameworkDTO
                };

                return await _mediator.Send(createPrFrameWorkCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPut]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> UpdateFramework(UpdatePrFrameworkDTO updatePrFrameworkDto)
        {
            try
            {
                UpdatePrFrameWorkCommand updatePrFrameWork = new UpdatePrFrameWorkCommand
                {
                    updatePrFramework = updatePrFrameworkDto
                };

                return await _mediator.Send(updatePrFrameWork);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpDelete]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> DeleteFramework([FromODataUri] int key)
        {
            try
            {
                DeletePrFrameworkCommand deletePrFrameworkCommand = new DeletePrFrameworkCommand
                {
                    deleteLanguage = new DeleteByIdDTO { Id = key }
                };

                return await _mediator.Send(deletePrFrameworkCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}

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

        [EnableQuery]
        [HttpGet("by")]
        public async Task<IActionResult> GetById([FromODataUri] int key)
        {
            try
            {
                ByIdLocaleQuery localeQuery = new ByIdLocaleQuery
                {
                    Id = key
                };

                SingleResult<SupportedLocale> language = await _mediator.Send(localeQuery);

                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> CreateLocale(CreateLocaleDTO createLocale)
        {
            try
            {
                CreateLocaleCommand createLocaleCommand = new CreateLocaleCommand
                {
                    createLocale = createLocale
                };

                return await _mediator.Send(createLocaleCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPut]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> UpdateLocale(UpdateLocaleDTO updateLocale)
        {
            try
            {
                UpdateLocaleCommand updateLocaleCommand = new UpdateLocaleCommand
                {
                    updateLocale = updateLocale
                };

                return await _mediator.Send(updateLocaleCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpDelete]
        [ServiceFilter(typeof(BasicAdminAuthFilter))]
        public async Task<IActionResult> DeleteLocale([FromODataUri] int key)
        {
            try
            {
                DeleteLocaleCommand deleteLocaleCommand = new DeleteLocaleCommand
                {
                    deleteLocale = new DeleteByIdDTO { Id = key }
                };

                return await _mediator.Send(deleteLocaleCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

    }
}

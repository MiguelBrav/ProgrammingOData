using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ProgrammingOData.API.Aggregator.Interfaces;
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
        private readonly ISupportedLocalesAggregator _aggregator;

        public SupportedLocalesController(ISupportedLocalesAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                AllSupportedLocalesQuery allLocalesQuery = new AllSupportedLocalesQuery();

                IQueryable<SupportedLocale> locales = await _aggregator.AllSupportedLocalesQuery(allLocalesQuery);
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

                SingleResult<SupportedLocale> language = await _aggregator.ByIdLocaleQuery(localeQuery);
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

                return await _aggregator.CreateLocale(createLocaleCommand);
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

                return await _aggregator.UpdateLocale(updateLocaleCommand);
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

                return await _aggregator.DeleteLocale(deleteLocaleCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

    }
}

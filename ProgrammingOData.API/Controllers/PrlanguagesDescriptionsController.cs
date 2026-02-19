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
    public class PrlanguagesDescriptionsController : ODataController
    {
        private readonly IPrlanguagesDescriptionsAggregator _aggregator;

        public PrlanguagesDescriptionsController(IPrlanguagesDescriptionsAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                AllPrLanguageDescriptionsQuery allLanguagesDescriptionsQuery = new AllPrLanguageDescriptionsQuery();

                IQueryable<PrLanguageDescription> languagesDescriptions = await _aggregator.AllPrLanguageDescriptionsQuery(allLanguagesDescriptionsQuery);
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

                SingleResult<PrLanguageDescription> language = await _aggregator.ByIdPrLanguageDescriptionQuery(prLanguageDescQuery);
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

                return await _aggregator.CreatePrLanguageDesc(createPrLanguageDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpPut]
        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        public async Task<IActionResult> UpdateLanguageDesc(UpdatePrLanguageDescDTO updateLanguageDesc)
        {
            try
            {
                UpdatePrLanguageDescCommand updatePrLanguageDescCommand = new UpdatePrLanguageDescCommand
                {
                    updateLanguageDesc = updateLanguageDesc
                };

                return await _aggregator.UpdatePrLanguageDesc(updatePrLanguageDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }    

        [HttpDelete]
        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        public async Task<IActionResult> DeleteLanguageDescription([FromODataUri] int key)
        {
            try
            {
                DeletePrLanguageDescCommand deletePrLanguageDescCommand = new DeletePrLanguageDescCommand
                {
                    deleteLanguage = new DeleteByIdDTO { Id = key }
                };

                return await _aggregator.DeletePrLanguageDesc(deletePrLanguageDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }
    }
}

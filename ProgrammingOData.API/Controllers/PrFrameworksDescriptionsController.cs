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
    public class PrFrameworksDescriptionsController : ODataController
    {
        private readonly IPrFrameworksDescriptionsAggregator _aggregator;

        public PrFrameworksDescriptionsController(IPrFrameworksDescriptionsAggregator aggregator)
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
                AllPrFrameworkDescQuery allPrFrameworkDescQuery = new AllPrFrameworkDescQuery();

                IQueryable<PrFrameworkDescription> frameworkdsDesc = await _aggregator.AllPrFrameworkDescQuery(allPrFrameworkDescQuery);
                return Ok(frameworkdsDesc);
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
                ByIdPrFrameworkDescriptionQuery prFrameworDesckQuery = new ByIdPrFrameworkDescriptionQuery
                {
                    Id = key
                };

                SingleResult<PrFrameworkDescription> frameworkDesc = await _aggregator.ByIdPrFrameworkDescriptionQuery(prFrameworDesckQuery);
                return Ok(frameworkDesc);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        [HttpPost]
        public async Task<IActionResult> CreateFrameworkDescription(CreatePrFrameworkDescDTO createFrameworkDescription)
        {
            try
            {
                CreatePrFrameWorkDescCommand createPrFrameWorkDescCommand = new CreatePrFrameWorkDescCommand
                {
                    CreatePrFrameWorkDesc = createFrameworkDescription
                };

                return await _aggregator.CreatePrFrameworkDesc(createPrFrameWorkDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        [HttpPut]
        public async Task<IActionResult> UpdateFrameworkDescription(UpdatePrFrameworkDescDTO updatePrFrameworkDesc)
        {
            try
            {
                UpdatePrFrameWorkDescCommand createPrFrameWorkDescCommand = new UpdatePrFrameWorkDescCommand
                {
                    UpdatePrFrameworkDesc = updatePrFrameworkDesc
                };

                return await _aggregator.UpdatePrFrameworkDesc(createPrFrameWorkDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

        [HttpDelete]
        [ServiceFilter(typeof(BasicEditorAuthFilter))]
        public async Task<IActionResult> DeleteFrameworkDescription([FromODataUri] int key)
        {
            try
            {
                DeletePrFrameworkDescCommand deletePrFrameworkDescCommand = new DeletePrFrameworkDescCommand
                {
                    deleteFramework = new DeleteByIdDTO { Id = key }
                };

                return await _aggregator.DeletePrFrameworkDesc(deletePrFrameworkDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

    }
}

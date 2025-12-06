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
    public class PrFrameworksDescriptionsController : ODataController
    {
        private IMediator _mediator;

        public PrFrameworksDescriptionsController(IMediator mediator)
        {
            _mediator = mediator;
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

                return await _mediator.Send(createPrFrameWorkDescCommand);
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

                return await _mediator.Send(createPrFrameWorkDescCommand);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred " + ex);
            }
        }

    }
}

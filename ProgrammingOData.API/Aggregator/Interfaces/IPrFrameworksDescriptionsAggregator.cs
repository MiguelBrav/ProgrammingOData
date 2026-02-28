using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingOData.API.Aggregator.Interfaces;

public interface IPrFrameworksDescriptionsAggregator
{
    Task<IQueryable<PrFrameworkDescription>> AllPrFrameworkDescQuery(AllPrFrameworkDescQuery request);
    Task<SingleResult<PrFrameworkDescription>> ByIdPrFrameworkDescriptionQuery(ByIdPrFrameworkDescriptionQuery request);
    Task<IActionResult> CreatePrFrameworkDesc(CreatePrFrameWorkDescCommand request);
    Task<IActionResult> UpdatePrFrameworkDesc(UpdatePrFrameWorkDescCommand request);
    Task<IActionResult> DeletePrFrameworkDesc(DeletePrFrameworkDescCommand request);
}

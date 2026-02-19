using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingOData.API.Aggregator.Interfaces;

public interface IPrFrameworksAggregator
{
    Task<IQueryable<PrFramework>> AllPrFrameworkQuery(AllPrFrameworkQuery request);
    Task<SingleResult<PrFramework>> ByIdPrFrameworkQuery(ByIdPrFrameworkQuery request);
    Task<IActionResult> CreatePrFramework(CreatePrFrameWorkCommand request);
    Task<IActionResult> UpdatePrFramework(UpdatePrFrameWorkCommand request);
    Task<IActionResult> DeletePrFramework(DeletePrFrameworkCommand request);
}

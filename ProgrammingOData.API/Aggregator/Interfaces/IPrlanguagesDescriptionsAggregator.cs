using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingOData.API.Aggregator.Interfaces;

public interface IPrlanguagesDescriptionsAggregator
{
    Task<IQueryable<PrLanguageDescription>> AllPrLanguageDescriptionsQuery(AllPrLanguageDescriptionsQuery request);
    Task<SingleResult<PrLanguageDescription>> ByIdPrLanguageDescriptionQuery(ByIdPrLanguageDescriptionQuery request);
    Task<IActionResult> CreatePrLanguageDesc(CreatePrLanguageDescCommand request);
    Task<IActionResult> UpdatePrLanguageDesc(UpdatePrLanguageDescCommand request);
    Task<IActionResult> DeletePrLanguageDesc(DeletePrLanguageDescCommand request);
}

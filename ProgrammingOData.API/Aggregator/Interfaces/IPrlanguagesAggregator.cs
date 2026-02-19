using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingOData.API.Aggregator.Interfaces;

public interface IPrlanguagesAggregator
{
    Task<IQueryable<PrLanguage>> AllPrLanguageQuery(AllPrLanguageQuery request);
    Task<SingleResult<PrLanguage>> ByIdPrLanguageQuery(ByIdPrLanguageQuery request);
    Task<IActionResult> CreatePrLanguage(CreatePrLanguageCommand request);
    Task<IActionResult> UpdatePrLanguage(UpdatePrLanguageCommand request);
    Task<IActionResult> DeletePrLanguage(DeletePrLanguageCommand request);
}

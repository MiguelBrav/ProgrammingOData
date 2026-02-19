using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingOData.API.Aggregator.Interfaces;

public interface ISupportedLocalesAggregator
{
    Task<IQueryable<SupportedLocale>> AllSupportedLocalesQuery(AllSupportedLocalesQuery request);
    Task<SingleResult<SupportedLocale>> ByIdLocaleQuery(ByIdLocaleQuery request);
    Task<IActionResult> CreateLocale(CreateLocaleCommand request);
    Task<IActionResult> UpdateLocale(UpdateLocaleCommand request);
    Task<IActionResult> DeleteLocale(DeleteLocaleCommand request);
}

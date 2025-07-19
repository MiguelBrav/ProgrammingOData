using MediatR;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class AllPrFrameworkQuery : IRequest<IQueryable<PrFramework>>
{
    public string? Locale {  get; set; }
}

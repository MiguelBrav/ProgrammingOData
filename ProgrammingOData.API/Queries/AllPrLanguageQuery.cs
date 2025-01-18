using MediatR;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class AllPrLanguageQuery : IRequest<IQueryable<PrLanguage>>
{
    public string? Locale {  get; set; }
}

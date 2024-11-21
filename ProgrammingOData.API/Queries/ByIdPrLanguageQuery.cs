using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class ByIdPrLanguageQuery : IRequest<SingleResult<PrLanguage>>
{
    public int Id { get; set; }
}

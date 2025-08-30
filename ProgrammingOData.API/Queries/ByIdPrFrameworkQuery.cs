using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class ByIdPrFrameworkQuery : IRequest<SingleResult<PrFramework>>
{
    public int Id { get; set; }
    public string? Locale { get; set; }
}

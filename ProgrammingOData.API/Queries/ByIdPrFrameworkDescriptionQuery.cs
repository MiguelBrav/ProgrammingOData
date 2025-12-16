using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class ByIdPrFrameworkDescriptionQuery : IRequest<SingleResult<PrFrameworkDescription>>
{
    public int Id { get; set; }
    public string? Locale { get; set; }
}

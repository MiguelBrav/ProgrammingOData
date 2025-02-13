using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class ByIdPrLanguageDescriptionQuery : IRequest<SingleResult<PrLanguageDescription>>
{
    public int Id { get; set; }

}

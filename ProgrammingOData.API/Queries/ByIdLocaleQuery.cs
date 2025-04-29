using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class ByIdLocaleQuery : IRequest<SingleResult<SupportedLocale>>
{
    public int Id { get; set; }
}

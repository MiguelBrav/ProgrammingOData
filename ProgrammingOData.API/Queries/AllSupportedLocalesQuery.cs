using MediatR;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class AllSupportedLocalesQuery : IRequest<IQueryable<SupportedLocale>>
{
}

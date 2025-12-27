using MediatR;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class AllPrFrameworkDescQuery : IRequest<IQueryable<PrFrameworkDescription>>
{

}

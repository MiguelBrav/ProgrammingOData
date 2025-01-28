using MediatR;
using ProgrammingOData.Models.Entities;

namespace ProgrammingOData.API.Queries;
public class AllPrLanguageDescriptionsQuery : IRequest<IQueryable<PrLanguageDescription>>
{

}

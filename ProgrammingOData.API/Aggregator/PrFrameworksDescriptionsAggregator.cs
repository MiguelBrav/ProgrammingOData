using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Aggregator.Interfaces;
using UseCaseCore.UseCases;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingOData.API.Aggregator;

public class PrFrameworksDescriptionsAggregator : IPrFrameworksDescriptionsAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllPrFrameworkDescQueryHandler _allHandler;
    private readonly ByIdPrFrameworkDescriptionQueryHandler _byIdHandler;
    private readonly CreatePrFrameWorkDescCommandHandler _createHandler;
    private readonly UpdatePrFrameWorkDescCommandHandler _updateHandler;
    private readonly DeletePrFrameworkDescCommandHandler _deleteHandler;

    public PrFrameworksDescriptionsAggregator(
        UseCaseDispatcher dispatcher,
        AllPrFrameworkDescQueryHandler allHandler,
        ByIdPrFrameworkDescriptionQueryHandler byIdHandler,
        CreatePrFrameWorkDescCommandHandler createHandler,
        UpdatePrFrameWorkDescCommandHandler updateHandler,
        DeletePrFrameworkDescCommandHandler deleteHandler)
    {
        _dispatcher = dispatcher;
        _allHandler = allHandler;
        _byIdHandler = byIdHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public async Task<IQueryable<PrFrameworkDescription>> AllPrFrameworkDescQuery(AllPrFrameworkDescQuery request)
        => await _dispatcher.Dispatch(_allHandler, request);

    public async Task<SingleResult<PrFrameworkDescription>> ByIdPrFrameworkDescriptionQuery(ByIdPrFrameworkDescriptionQuery request)
        => await _dispatcher.Dispatch(_byIdHandler, request);

    public async Task<IActionResult> CreatePrFrameworkDesc(CreatePrFrameWorkDescCommand request)
        => await _dispatcher.Dispatch(_createHandler, request);

    public async Task<IActionResult> UpdatePrFrameworkDesc(UpdatePrFrameWorkDescCommand request)
        => await _dispatcher.Dispatch(_updateHandler, request);

    public async Task<IActionResult> DeletePrFrameworkDesc(DeletePrFrameworkDescCommand request)
        => await _dispatcher.Dispatch(_deleteHandler, request);
}

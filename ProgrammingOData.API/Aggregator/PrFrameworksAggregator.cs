using ProgrammingOData.API.Commands;
using ProgrammingOData.API.Queries;
using ProgrammingOData.Models.Entities;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.Mvc;
using ProgrammingOData.API.Aggregator.Interfaces;
using UseCaseCore.UseCases;

namespace ProgrammingOData.API.Aggregator;

public class PrFrameworksAggregator : IPrFrameworksAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllPrFrameworkQueryHandler _allHandler;
    private readonly ByIdPrFrameworkQueryHandler _byIdHandler;
    private readonly CreatePrFrameWorkCommandHandler _createHandler;
    private readonly UpdatePrFrameWorkCommandHandler _updateHandler;
    private readonly DeletePrFrameworkCommandHandler _deleteHandler;

    public PrFrameworksAggregator(
        UseCaseDispatcher dispatcher,
        AllPrFrameworkQueryHandler allHandler,
        ByIdPrFrameworkQueryHandler byIdHandler,
        CreatePrFrameWorkCommandHandler createHandler,
        UpdatePrFrameWorkCommandHandler updateHandler,
        DeletePrFrameworkCommandHandler deleteHandler)
    {
        _dispatcher = dispatcher;
        _allHandler = allHandler;
        _byIdHandler = byIdHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public async Task<IQueryable<PrFramework>> AllPrFrameworkQuery(AllPrFrameworkQuery request)
        => await _dispatcher.Dispatch(_allHandler, request);

    public async Task<SingleResult<PrFramework>> ByIdPrFrameworkQuery(ByIdPrFrameworkQuery request)
        => await _dispatcher.Dispatch(_byIdHandler, request);

    public async Task<IActionResult> CreatePrFramework(CreatePrFrameWorkCommand request)
        => await _dispatcher.Dispatch(_createHandler, request);

    public async Task<IActionResult> UpdatePrFramework(UpdatePrFrameWorkCommand request)
        => await _dispatcher.Dispatch(_updateHandler, request);

    public async Task<IActionResult> DeletePrFramework(DeletePrFrameworkCommand request)
        => await _dispatcher.Dispatch(_deleteHandler, request);
}

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

public class PrlanguagesDescriptionsAggregator : IPrlanguagesDescriptionsAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllPrLanguageDescriptionsQueryHandler _allHandler;
    private readonly ByIdPrLanguageDescriptionQueryHandler _byIdHandler;
    private readonly CreatePrLanguageDescCommandHandler _createHandler;
    private readonly UpdatePrLanguageDescCommandHandler _updateHandler;
    private readonly DeletePrLanguageDescCommandHandler _deleteHandler;

    public PrlanguagesDescriptionsAggregator(
        UseCaseDispatcher dispatcher,
        AllPrLanguageDescriptionsQueryHandler allHandler,
        ByIdPrLanguageDescriptionQueryHandler byIdHandler,
        CreatePrLanguageDescCommandHandler createHandler,
        UpdatePrLanguageDescCommandHandler updateHandler,
        DeletePrLanguageDescCommandHandler deleteHandler)
    {
        _dispatcher = dispatcher;
        _allHandler = allHandler;
        _byIdHandler = byIdHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public async Task<IQueryable<PrLanguageDescription>> AllPrLanguageDescriptionsQuery(AllPrLanguageDescriptionsQuery request)
        => await _dispatcher.Dispatch(_allHandler, request);

    public async Task<SingleResult<PrLanguageDescription>> ByIdPrLanguageDescriptionQuery(ByIdPrLanguageDescriptionQuery request)
        => await _dispatcher.Dispatch(_byIdHandler, request);

    public async Task<IActionResult> CreatePrLanguageDesc(CreatePrLanguageDescCommand request)
        => await _dispatcher.Dispatch(_createHandler, request);

    public async Task<IActionResult> UpdatePrLanguageDesc(UpdatePrLanguageDescCommand request)
        => await _dispatcher.Dispatch(_updateHandler, request);

    public async Task<IActionResult> DeletePrLanguageDesc(DeletePrLanguageDescCommand request)
        => await _dispatcher.Dispatch(_deleteHandler, request);
}

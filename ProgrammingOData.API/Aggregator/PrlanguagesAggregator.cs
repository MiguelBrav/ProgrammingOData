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

public class PrlanguagesAggregator : IPrlanguagesAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllPrLanguageQueryHandler _allHandler;
    private readonly ByIdPrLanguageQueryHandler _byIdHandler;
    private readonly CreatePrLanguageCommandHandler _createHandler;
    private readonly UpdatePrLanguageCommandHandler _updateHandler;
    private readonly DeletePrLanguageCommandHandler _deleteHandler;

    public PrlanguagesAggregator(
        UseCaseDispatcher dispatcher,
        AllPrLanguageQueryHandler allHandler,
        ByIdPrLanguageQueryHandler byIdHandler,
        CreatePrLanguageCommandHandler createHandler,
        UpdatePrLanguageCommandHandler updateHandler,
        DeletePrLanguageCommandHandler deleteHandler)
    {
        _dispatcher = dispatcher;
        _allHandler = allHandler;
        _byIdHandler = byIdHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public async Task<IQueryable<PrLanguage>> AllPrLanguageQuery(AllPrLanguageQuery request)
        => await _dispatcher.Dispatch(_allHandler, request);

    public async Task<SingleResult<PrLanguage>> ByIdPrLanguageQuery(ByIdPrLanguageQuery request)
        => await _dispatcher.Dispatch(_byIdHandler, request);

    public async Task<IActionResult> CreatePrLanguage(CreatePrLanguageCommand request)
        => await _dispatcher.Dispatch(_createHandler, request);

    public async Task<IActionResult> UpdatePrLanguage(UpdatePrLanguageCommand request)
        => await _dispatcher.Dispatch(_updateHandler, request);

    public async Task<IActionResult> DeletePrLanguage(DeletePrLanguageCommand request)
        => await _dispatcher.Dispatch(_deleteHandler, request);
}

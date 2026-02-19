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

public class SupportedLocalesAggregator : ISupportedLocalesAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllSupportedLocalesQueryHandler _allHandler;
    private readonly ByIdLocaleQueryHandler _byIdHandler;
    private readonly CreateLocaleCommandHandler _createHandler;
    private readonly UpdateLocaleCommandHandler _updateHandler;
    private readonly DeleteLocaleCommandHandler _deleteHandler;

    public SupportedLocalesAggregator(
        UseCaseDispatcher dispatcher,
        AllSupportedLocalesQueryHandler allHandler,
        ByIdLocaleQueryHandler byIdHandler,
        CreateLocaleCommandHandler createHandler,
        UpdateLocaleCommandHandler updateHandler,
        DeleteLocaleCommandHandler deleteHandler)
    {
        _dispatcher = dispatcher;
        _allHandler = allHandler;
        _byIdHandler = byIdHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public async Task<IQueryable<SupportedLocale>> AllSupportedLocalesQuery(AllSupportedLocalesQuery request)
        => await _dispatcher.Dispatch(_allHandler, request);

    public async Task<SingleResult<SupportedLocale>> ByIdLocaleQuery(ByIdLocaleQuery request)
        => await _dispatcher.Dispatch(_byIdHandler, request);

    public async Task<IActionResult> CreateLocale(CreateLocaleCommand request)
        => await _dispatcher.Dispatch(_createHandler, request);

    public async Task<IActionResult> UpdateLocale(UpdateLocaleCommand request)
        => await _dispatcher.Dispatch(_updateHandler, request);

    public async Task<IActionResult> DeleteLocale(DeleteLocaleCommand request)
        => await _dispatcher.Dispatch(_deleteHandler, request);
}

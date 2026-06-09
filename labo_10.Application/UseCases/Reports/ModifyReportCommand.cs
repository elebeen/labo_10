using labo_10.Domain.Interfaces;
using MediatR;

namespace labo_10.UseCases.Reports;

public class ModifyReportCommand : IRequest<Unit> { }

internal sealed record ModifyReportCommandHandler : IRequestHandler<ModifyReportCommand, Unit>
{
    public readonly IClosedXmlService _closedXmlService;

    public ModifyReportCommandHandler(IClosedXmlService closedXmlService)
    {
        _closedXmlService = closedXmlService;
    }

    public Task<Unit> Handle(ModifyReportCommand request, CancellationToken cancellationToken)
    {
        _closedXmlService.ModifyArchive();
        return Task.FromResult(Unit.Value);
    }
}
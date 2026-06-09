using labo_10.Domain.Interfaces;
using MediatR;

namespace labo_10.UseCases.Reports;

public class UsersReportCommand : IRequest<Unit> { }

internal sealed record UsersReportCommandHandler : IRequestHandler<UsersReportCommand, Unit>
{
    public readonly IClosedXmlService _closedXmlService;

    public UsersReportCommandHandler(IClosedXmlService closedXmlService)
    {
        _closedXmlService = closedXmlService;
    }

    public Task<Unit> Handle(UsersReportCommand request, CancellationToken cancellationToken)
    {
        _closedXmlService.FirstExample();
        return Task.FromResult(new Unit());
    }
}
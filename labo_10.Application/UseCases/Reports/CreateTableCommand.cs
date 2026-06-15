using labo_10.Domain.Interfaces.Services;
using MediatR;

namespace labo_10.UseCases.Reports;

public class CreateTableCommand : IRequest<Unit>{}

internal sealed record CreateTableCommandHandler : IRequestHandler<CreateTableCommand, Unit>
{
    public readonly IClosedXmlService _closedXmlService;

    public CreateTableCommandHandler(IClosedXmlService closedXmlService)
    {
        _closedXmlService = closedXmlService;
    }

    public Task<Unit> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        _closedXmlService.ThirdExample();
        return Task.FromResult(new Unit());
    }
}
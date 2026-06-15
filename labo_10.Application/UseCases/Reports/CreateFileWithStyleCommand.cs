using labo_10.Domain.Interfaces.Services;
using MediatR;

namespace labo_10.UseCases.Reports;

public class CreateFileWithStyleCommand : IRequest<Unit>{}

internal sealed record CreateFileWithStyleCommandHandler : IRequestHandler<CreateFileWithStyleCommand, Unit>
{
    public readonly IClosedXmlService _closedXmlService;

    public CreateFileWithStyleCommandHandler(IClosedXmlService closedXmlService)
    {
        _closedXmlService = closedXmlService;
    }

    public Task<Unit> Handle(CreateFileWithStyleCommand request, CancellationToken cancellationToken)
    {
        _closedXmlService.FourthExample();
        return Task.FromResult(new Unit());
    }
}
using labo_10.Domain.Dto;
using labo_10.Domain.Interfaces.Repositories;
using labo_10.Domain.Interfaces.Services;
using MediatR;

namespace labo_10.UseCases.Reports;

public class GenerateTicketsReportCommand : IRequest<FileDownloadResponse> { }

internal sealed record GenerateTicketsReportCommandHandler : IRequestHandler<GenerateTicketsReportCommand, FileDownloadResponse>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IClosedXmlService _closedXmlService;

    public GenerateTicketsReportCommandHandler(ITicketRepository ticketRepository, IClosedXmlService closedXmlService)
    {
        _ticketRepository = ticketRepository;
        _closedXmlService = closedXmlService;
    }

    public async Task<FileDownloadResponse> Handle(GenerateTicketsReportCommand request, CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.GetAllWithDetailsAsync(cancellationToken);
        byte[] fileBytes = _closedXmlService.GenerateTicketsReport(tickets);

        // Toda la metadata del archivo se define en la capa de aplicación
        string fileName = $"Reporte_Tickets_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return new FileDownloadResponse(fileBytes, contentType, fileName);
    }
}
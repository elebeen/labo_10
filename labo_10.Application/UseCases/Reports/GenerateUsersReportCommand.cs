using labo_10.Domain.Dto;
using labo_10.Domain.Interfaces.Repositories;
using labo_10.Domain.Interfaces.Services;
using MediatR;

namespace labo_10.UseCases.Reports;

public class GenerateUsersReportCommand : IRequest<FileDownloadResponse> { } // Cambiado el retorno

internal sealed record GenerateUsersReportCommandHandler : IRequestHandler<GenerateUsersReportCommand, FileDownloadResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IClosedXmlService _closedXmlService;

    public GenerateUsersReportCommandHandler(IUserRepository userRepository, IClosedXmlService closedXmlService)
    {
        _userRepository = userRepository;
        _closedXmlService = closedXmlService;
    }

    public async Task<FileDownloadResponse> Handle(GenerateUsersReportCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllWithRolesAsync(cancellationToken);
        byte[] fileBytes = _closedXmlService.GenerateUsersRolesReport(users);

        string fileName = $"Reporte_Usuarios_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return new FileDownloadResponse(fileBytes, contentType, fileName);
    }
}
using labo_10.Domain.Interfaces;
using labo_10.Domain.Interfaces.Repositories;
using labo_10.Domain.Models;
using MediatR;

namespace labo_10.UseCases.Maintenance;

public class CleanSystemCommand : IRequest<bool> { }

internal sealed record CleanSystemCommandHandler : IRequestHandler<CleanSystemCommand, bool>
{
    private readonly IRepository<User> _userRepository; // Puedes inyectar lo que necesites

    public CleanSystemCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(CleanSystemCommand request, CancellationToken cancellationToken)
    {
        // Aquí colocas la lógica personalizada de tu tarea
        Console.WriteLine($"[Hangfire Job] Iniciando mantenimiento del sistema a las: {DateTime.Now}");
        
        // Ejemplo: Obtener cuántos usuarios hay en el sistema usando tu repositorio
        var users = await _userRepository.GetAllAsync();
        Console.WriteLine($"[Hangfire Job] Revisión completada. Usuarios actuales en el sistema: {users.Count()}");
        
        Console.WriteLine("[Hangfire Job] Mantenimiento finalizado exitosamente.");
        return true;
    }
}
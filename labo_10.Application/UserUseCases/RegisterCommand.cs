using MediatR;
using labo_10.Infrastructure.Repositories;
using labo_10.Domain.Models;

namespace labo_10.UserUseCases;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IRepository<User> _userRepository;

    public RegisterCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar por Email si el usuario ya existe
        var existingUser = await _userRepository.FindFirstAsync(u => u.Email == request.Email);
        if (existingUser != null) return false; 

        // 2. Hashear la contraseña
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3. Crear la entidad
        var newUser = new User()
        {
            UserId = Guid.NewGuid(),
            Username = request.Nombre,
            Email = request.Email, 
            PasswordHash = hashedPassword 
        };
    
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();
    
        return true;
    }
}
using labo_10.Domain.Interfaces;
using labo_10.Domain.Interfaces.Repositories;
using labo_10.Domain.Models;
using MediatR;

namespace labo_10.UseCases.Auth.Commands;

public class RegisterUserCommand : IRequest<bool>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Nombre { get; set; }
}

internal sealed record RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly IRepository<User> _userRepository;

    public RegisterUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar si el usuario ya existe
        var existingUser = await _userRepository.FindFirstAsync(u => u.Username == request.Nombre);
        if (existingUser != null) 
        {
            // Lanzamos una excepción en lugar de retornar false
            throw new ArgumentException("El usuario ya existe o hubo un error.");
        }

        // 2. Encriptar la contraseña
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3. Crear el nuevo usuario
        var newUser = new User()
        {
            Email = request.Email,
            Username = request.Nombre,
            PasswordHash = hashedPassword 
        };
        
        // 4. Guardar
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync(); 
        
        return true;
    }
}
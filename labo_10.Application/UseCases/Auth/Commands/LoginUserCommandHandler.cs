using labo_10.Domain.Interfaces;
using labo_10.Domain.Models;
using MediatR;

namespace labo_10.UseCases.Auth.Commands;

public class LoginUserCommand : IRequest<string?>
{
    public required string Email { get; set; } 
    public required string Password { get; set; }
}

internal sealed record LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly IRepository<User> _userRepository;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IRepository<User> userRepository,  IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar el usuario
        var existingUser = await _userRepository.FindFirstAsync(u => u.Email == request.Email);
        
        if (existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash))
        {
            // return Task.FromResult<string?>(null); // Credenciales inválidas
            throw new ArgumentException("Credenciales inválidas");
        }

        // 2. Generar el Token
        //string role = "Administrator";
        const string role = "Administrator";
        var token = _jwtService.GenerateJwtToken(existingUser.UserId.ToString(), request.Email, role);

        return token;
    }
}
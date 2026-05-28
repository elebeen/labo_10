using MediatR;

namespace labo_10.UseCases.Auth.Commands;

public class RegisterUserCommand : IRequest<bool>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Nombre { get; set; }
}
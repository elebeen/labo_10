using Hangfire;
using labo_10.Infrastructure.Services;
using labo_10.UseCases.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace labo_10.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        await _mediator.Send(command);
        RecurringJob.AddOrUpdate("job-notificacion-diaria",
            () => new NotificationService().SendNotification("usuario_diario"), Cron.Daily);
        return Ok(new { message = "Usuario registrado exitosamente" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        BackgroundJob.Schedule(() => new NotificationService().SendNotification("usuario2")
            , TimeSpan.FromMinutes(1));
        return Ok(new { token });
    }
    
    [Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "ga")]
    [HttpGet("admin")]
    public IActionResult GetAdmin()
    {
        BackgroundJob.Enqueue(() => new NotificationService().SendNotification("usuario1"));
        return Ok("solo admins");
    }
}
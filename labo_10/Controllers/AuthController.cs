using labo_10.UseCases.Auth.Commands;
using labo_10.UseCases.Reports;
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
        return Ok(new { message = "Usuario registrado exitosamente" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { token });
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpGet("admin")]
    public IActionResult GetAdmin()
    {
        return Ok("solo admins");
    }

    [HttpPost("reports")]
    public async Task<OkResult> MakeReports(UsersReportCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("editReports")]
    public async Task<OkResult> EditReports(ModifyReportCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("table")]
    public async Task<OkResult> CreateTable(CreateTableCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("estilos")]
    public async Task<OkResult> CreateFileWithStyle(CreateFileWithStyleCommand command)
    {
        await  _mediator.Send(command);
        return Ok();
    }
}
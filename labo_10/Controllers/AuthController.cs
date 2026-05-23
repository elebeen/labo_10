using labo_10.Dto;
using labo_10.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace labo_10.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var success = await _authService.RegisterAsync(request);

        if (!success)
            return BadRequest("El usuario ya existe o hubo un error.");

        return Ok(new { message = "Usuario registrado exitosamente" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!_authService.ValidateUser(request))
        {
            return Unauthorized();
        }

        // 2. Definir roles/permisos (esto podría venir de tu BD)
        string role = "Administrator"; 

        // 3. Generar token con claims
        var token = _authService.GenerateJwtToken(request.Email, role);

        return Ok(new { token });
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpGet("admin")]
    public IActionResult GetAdmin()
    {
        return Ok("solo admins");
    }
}
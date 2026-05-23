using Microsoft.AspNetCore.Mvc;

namespace labo_10.Controllers;

/*[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase 
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Este endpoint solo es accesible si el cliente envía un JWT válido
        // Además, puedes acceder a los datos del usuario autenticado desde el HttpContext si lo necesitas:
        var userEmail = User.Identity?.Name; 

        // Suponiendo que tu servicio obtiene actividades
        // var activities = await _activityService.GetActivitiesAsync();
        
        return Ok(new { Message = $"Lista de actividades para {userEmail}", Data = new List<string>() });
    }

    [HttpPost]
    // [Authorize] // También puedes ponerlo a nivel de método si el controlador fuera público
    public IActionResult Create()
    {
        return Ok(new { Message = "Actividad creada con éxito" });
    }
}*/
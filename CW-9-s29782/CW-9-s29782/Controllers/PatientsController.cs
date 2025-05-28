using CW_9_s29782.Exceptions;
using CW_9_s29782.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_9_s29782.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientInfo([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPatientDetailsAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
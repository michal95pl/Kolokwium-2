using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientSubscription(int idClient)
    {
        if (!await _clientService.ClientExists(idClient))
            return NotFound("client not exists");

        var data = _clientService.GetClientSubscription(idClient);

        return Ok(data);
    }
}
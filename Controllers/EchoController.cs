using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models;
using woodgroveapi.Services;

namespace woodgroveapi.Controllers;


//[Authorize]
[ApiController]
[Route("[controller]")]
public class EchoController : ControllerBase
{
    private readonly ILogger<EchoController> _logger;

    public EchoController(ILogger<EchoController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "TokenIssuanceStart")]
    public async Task<object> PostAsync()
    {
        Debugger.PrintDebugInfo(this, _logger);

        string requestBody = await new StreamReader(this.Request.Body).ReadToEndAsync();

        _logger.LogInformation( $"#### {requestBody}");

        return null;
    }
}


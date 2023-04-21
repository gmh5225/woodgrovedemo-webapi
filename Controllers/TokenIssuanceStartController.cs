using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Request;
using woodgroveapi.Models.Response;

namespace woodgroveapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenIssuanceStartController : ControllerBase
{
    private readonly ILogger<TokenIssuanceStartController> _logger;

    public TokenIssuanceStartController(ILogger<TokenIssuanceStartController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "TokenIssuanceStart")]
    public async Task<ResponseData> PostAsync()
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(this.Request.Body).ReadToEndAsync();

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        RequestData data = await JsonSerializer.DeserializeAsync<RequestData>(stream);

        // Read the correlation ID from the Azure AD  request    
        string correlationId = data.data.authenticationContext.correlationId; ;

        // Claims to return to Azure AD
        ResponseData r = new ResponseData();
        r.data.actions[0].claims.CorrelationId = correlationId;
        r.data.actions[0].claims.ApiVersion = "1.0.0";
        Random random = new Random();
        r.data.actions[0].claims.LoyaltyNumber = random.Next(123467, 999989).ToString();
        r.data.actions[0].claims.CustomRoles.Add("Writer");
        r.data.actions[0].claims.CustomRoles.Add("Editor");
        return r;
    }
}

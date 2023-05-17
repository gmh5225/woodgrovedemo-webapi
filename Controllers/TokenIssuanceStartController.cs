using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Request;
using woodgroveapi.Models.Response;
using woodgroveapi.Services;

namespace woodgroveapi.Controllers;


[Authorize]
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
    public async Task<object> PostAsync()
    {
        Debugger.PrintDebugInfo(this, _logger);

        string requestBody = await new StreamReader(this.Request.Body).ReadToEndAsync();

        // Check whether the request contains an HTTP body
        if (string.IsNullOrEmpty(requestBody))
        {
            return new { Error = "Input JSON not found." };
        }

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        RequestData data;

        // Try to deserialize the input JSON
        try
        {
            data = await JsonSerializer.DeserializeAsync<RequestData>(stream);
        }
        catch (System.Exception)
        {
            return new { Error = "Invalid JSON object." };
        }

        // Read the correlation ID from the Azure AD  request    
        string correlationId = data.data.authenticationContext.correlationId; ;

        // Claims to return to Azure AD
        ResponseData r = new ResponseData(ResponseType.OnTokenIssuanceStartResponseData);
        r.AddAction(ActionType.ProvideClaimsForToken);
        r.data.actions[0].claims.CorrelationId = correlationId;
        r.data.actions[0].claims.ApiVersion = "1.0.3";

        // Loyalty program data
        Random random = new Random();
        string[] tiers = { "Silver", "Gold", "Platinum", "Diamond" };
        r.data.actions[0].claims.LoyaltyNumber = random.Next(123467, 999989).ToString();
        r.data.actions[0].claims.LoyaltySince = DateTime.Now.AddDays((-1) * random.Next(30, 365)).ToString("dd MMMM yyyy");
        r.data.actions[0].claims.LoyaltyTier = tiers[random.Next(0, tiers.Length)];

        // Custom roles
        r.data.actions[0].claims.CustomRoles.Add("Writer");
        r.data.actions[0].claims.CustomRoles.Add("Editor");
        return r;
    }
}

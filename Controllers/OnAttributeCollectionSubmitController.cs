using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Response;

namespace woodgroveapi.Controllers;

[ApiController]
[Route("[controller]")]
public class OnAttributeCollectionSubmitController : ControllerBase
{
    private readonly ILogger<OnAttributeCollectionSubmitController> _logger;

    public OnAttributeCollectionSubmitController(ILogger<OnAttributeCollectionSubmitController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "OnAttributeCollectionSubmit")]
    public async Task<ResponseData> PostAsync()
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(this.Request.Body).ReadToEndAsync();

        JsonNode data = JsonNode.Parse(requestBody);
    
        // Read the correlation ID from the Azure AD  request    
        string correlationId = data!["data"]!["authenticationContext"]!["correlationId"]!.GetValue<string>();;

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

using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Response;

namespace woodgroveapi.Controllers;

[ApiController]
[Route("[controller]")]
public class OnAttributeCollectionStartController : ControllerBase
{
    private readonly ILogger<OnAttributeCollectionStartController> _logger;

    public OnAttributeCollectionStartController(ILogger<OnAttributeCollectionStartController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "OnAttributeCollectionStart")]
    public async Task<ResponseData> PostAsync()
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(this.Request.Body).ReadToEndAsync();

        JsonNode data = JsonNode.Parse(requestBody);
    
        // Read the correlation ID from the Azure AD  request    
        string correlationId = data!["data"]!["authenticationContext"]!["correlationId"]!.GetValue<string>();;

        // Claims to return to Azure AD
        ResponseData r = new ResponseData(ResponseType.OnAttributeCollectionStartResponseData);
        r.AddAction(EventType.AttributeCollectionStart.SetPrefillValues);
        r.data.actions[0].inputs.jobTitle = "This is my test";
        return r;
    }
}

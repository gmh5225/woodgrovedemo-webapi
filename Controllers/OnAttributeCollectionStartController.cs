using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Request;
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

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        RequestData data = await JsonSerializer.DeserializeAsync<RequestData>(stream);

        // Read the correlation ID from the Azure AD  request    
        string correlationId = data.data.authenticationContext.correlationId; ;

        // Claims to return to Azure AD
        ResponseData r = new ResponseData(ResponseType.OnAttributeCollectionStartResponseData);
        r.AddAction(EventType.AttributeCollectionStart.SetPrefillValues);
        r.data.actions[0].inputs.jobTitle = "This is my test";
        return r;
    }
}

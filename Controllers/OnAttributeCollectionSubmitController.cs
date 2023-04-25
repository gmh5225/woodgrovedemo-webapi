using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Request;
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
        _logger.LogInformation("*********** OnAttributeCollectionSubmitController ***********");
        string requestBody = await new StreamReader(this.Request.Body).ReadToEndAsync();
        _logger.LogInformation(requestBody);

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        RequestData data = await JsonSerializer.DeserializeAsync<RequestData>(stream);


        // Read the correlation ID from the Azure AD  request    
        //string correlationId = data.data.authenticationContext.correlationId; ;

        // Errors to return to Azure AD
        ResponseData r = new ResponseData(ResponseType.OnAttributeCollectionSubmitResponseData);
        r.AddAction(ActionType.ShowValidationError);
        r.data.actions[0].message = "Please fix the following issues to proceed.";
        r.data.actions[0].attributeErrors.Add(new AttributeError("city", "We don't operate in this country"));
        return r;
    }
}

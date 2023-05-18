using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using woodgroveapi.Models.Request;
using woodgroveapi.Models.Response;

namespace woodgroveapi.Controllers;

[Authorize]
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
    public ResponsePayload PostAsync([FromBody] RequestPayload requestPayload)
    {
        // List of countries and cities where Woodgrove operates
        Dictionary<string, string> CountriesList = new Dictionary<string, string>();
        CountriesList.Add("au", " Sydney, Brisbane, Melbourne");
        CountriesList.Add("es", " Madrid, Barcelona, Seville");
        CountriesList.Add("us", " New York, Chicago, Boston, Seattle");

        // Message object to return to Azure AD
        ResponsePayload r = new ResponsePayload(ResponseType.OnAttributeCollectionSubmitResponseData);

        // Check the input attributes and return a generic error message
        if (requestPayload.data.userSignUpInfo == null ||
            requestPayload.data.userSignUpInfo.builtInAttributes == null ||
            requestPayload.data.userSignUpInfo.builtInAttributes.country == null ||
            requestPayload.data.userSignUpInfo.builtInAttributes.city == null)
        {
            r.AddAction(ActionType.ShowBlockPage);
            r.data.actions[0].message = "Can't find the country and/or city attributes.";
            return r;
        }

        // Demonstrates the use of block response
        if (requestPayload.data.userSignUpInfo.builtInAttributes.city.ToLower() == "block")
        {
            r.AddAction(ActionType.ShowBlockPage);
            r.data.actions[0].message = "You can't create an account with 'block' city.";
            return r;
        }

        // Check the country name in on the supported list
        if (!CountriesList.ContainsKey(requestPayload.data.userSignUpInfo.builtInAttributes.country))
        {
            r.AddAction(ActionType.ShowValidationError);
            r.data.actions[0].message = "Please fix the following issues to proceed.";
            r.data.actions[0].attributeErrors.Add(new AttributeError("country", $"We don't operate in '{requestPayload.data.userSignUpInfo.builtInAttributes.country}'"));
            return r;
        }

        // Get the countries' cities
        string cities = CountriesList[requestPayload.data.userSignUpInfo.builtInAttributes.country];

        // Check if the city provided by user in the supported list
        if (!(cities + ",").ToLower().Contains($" {requestPayload.data.userSignUpInfo.builtInAttributes.city.ToLower()},"))
        {
            r.AddAction(ActionType.ShowValidationError);
            r.data.actions[0].message = "Please fix the following issues to proceed.";
            r.data.actions[0].attributeErrors.Add(new AttributeError("city", $"We don't operate in this city. Please select one of the following:{cities}"));
        }
        else
        {
            // No issues have been identified, proceed to create the account
            r.AddAction(ActionType.ContinueWithDefaultBehavior);
        }

        return r;
    }
}

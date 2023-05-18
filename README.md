# Woodgrove groceries demo REST API

This dotnet C# Web API demonstrates how to use Microsoft Entra External ID'ss custom authentication extension for various events. 

## Endpoints

The sample code provides the following endpoints:

### Token issuance start

The *TokenIssuanceStart* event is triggered when a token is about to be issued to your application. When the event is triggered the custom extension REST API is called to fetch attributes from external systems. In this demo, the [TokenIssuanceStartController](./Controllers/TokenIssuanceStartController.cs) returns the following claims:

- **CorrelationId** the correlation ID that was sent by Azure AD to your REST API.
- **ApiVersion** a fixed value with your REST API version. This attribute can help you debug your REST API and check if your latest version is in used.
- **LoyaltyNumber** a random numeric value that represents an imaginary loyally number.
- **LoyaltySince** a random date that the that represents an imaginary time the user joined the loyalty program.
- **LoyaltyTier** a random string that the that represents an imaginary loyalty program tier.

The REST API endpoint URL:

```http
POST https://api.wggdemo.net/TokenIssuanceStart
```

### On attribute collection start

The *OnAttributeCollectionStart* is fired at the beginning of the attribute collection process and can be used to prevent the user from signing up (such as based on the domain they are authenticating from) or modify the initial attributes to be collected (such as including additional attributes to collect based on the user’s identity provider).

> [!IMPORTANT]
> The OnAttributeCollectionStart event type is not available yet.

### On attribute collection submit

OnAttributeCollectionSubmit event is fired after the user provides attribute information during signing up and can be used to validate the information provided by the user (such as an invitation code or partner number), modify the collected attributes (such as address validation), and either allow the user to continue in the journey or show a validation or block page.

> [!IMPORTANT]
> The OnAttributeCollectionSubmit event type is subject be to changed. Don't use it in your Microsoft Entra External ID tenant. 

This demo validates the city name, against a list of cities and countries we compiled. You can find the list of countries and cities in the [OnAttributeCollectionSubmitController](./Controllers/OnAttributeCollectionSubmitController.cs). 

The REST API endpoint URL:

```http
POST https://api.wggdemo.net/OnAttributeCollectionSubmit
```

## Protect access to your REST API

To ensure the communications between Microsoft Entra custom extension and your REST API are secured appropriately, Microsoft Entra External ID uses OAuth 2.0 client credentials grant flow to issue an access token for the resource application registered with your custom authentication extension. 

When the custom extension calls your REST API, it sends an HTTP Authorization header with a bearer token issued by Azure AD. You REST API validate the access token and its claims values. This example uses the [Microsoft.Identity.Web](https://www.nuget.org/packages/Microsoft.Identity.Web) library to validate the access token. 

In the [appsettings.json](./appsettings.json) file, update the following keys under the `AzureAd` element:

- **ClientId** the application ID that is associated with your custom extension. You can find this application under the API authentication in your custom extension.
- **Audience** same as above
- **TenantId** your tenant ID

This demo REST API can be used without authentication. So, you can use it in your Microsoft Entra External ID tenant. If you run your own REST API, uncomment the `[Authorize]` attribute in the controllers. The following example shows how a controller should look like:

```csharp
[Authorize]
[ApiController]
[Route("[controller]")]
public class TokenIssuanceStartController : ControllerBase
{
    // Rest of your code
}
```


## Data models

The code sample has the following data models:

- TokenIssuanceStart event [request](./Models/TokenIssuanceStartRequest.cs) and [response](./Models/TokenIssuanceStartResponse.cs)
- OnAttributeCollectionSubmit event [request](./Models/OnAttributeCollectionSubmitRequest.cs) and [response](./Models/OnAttributeCollectionSubmitResponse.cs)
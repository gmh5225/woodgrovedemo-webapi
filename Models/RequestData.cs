using System.Text.Json.Serialization;

namespace woodgroveapi.Models.Request
{

    public class RequestData
    {
        [JsonPropertyName("data")]
        public Data data { get; set; }
        public RequestData()
        {
            data = new Data();
        }
    }

    public class AuthenticationContext
    {
        public string correlationId { get; set; }
        public Client client { get; set; }
        public string protocol { get; set; }
        public ClientServicePrincipal clientServicePrincipal { get; set; }
        public ResourceServicePrincipal resourceServicePrincipal { get; set; }
        public User user { get; set; }
    }

    public class Client
    {
        public string ip { get; set; }
        public string locale { get; set; }
        public string market { get; set; }
    }

    public class ClientServicePrincipal
    {
        public string id { get; set; }
        public string appId { get; set; }
        public string appDisplayName { get; set; }
        public string displayName { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string tenantId { get; set; }
        public string authenticationEventListenerId { get; set; }
        public string customAuthenticationExtensionId { get; set; }
        public AuthenticationContext authenticationContext { get; set; }
        public UserSignUpInfo userSignUpInfo { get; set; }
    }

    public class ResourceServicePrincipal
    {
        public string id { get; set; }
        public string appId { get; set; }
        public string appDisplayName { get; set; }
        public string displayName { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public string source { get; set; }
        public Data data { get; set; }
    }

    public class User
    {
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string id { get; set; }
        public string mail { get; set; }
        public string preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string userType { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }

    // Sign-up classes
    public class UserSignUpInfo
    {
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public Country country { get; set; }
        public City city { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string value { get; set; }
        public string attributeType { get; set; }
    }

        public class City
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string value { get; set; }
        public string attributeType { get; set; }
    }
}
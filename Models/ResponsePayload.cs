using System.Text.Json.Serialization;

namespace woodgroveapi.Models.Response
{

    public class ResponsePayload
    {
        [JsonPropertyName("data")]
        public ResponseData data { get; set; }
        public ResponsePayload(string dataType)
        {
            data = new ResponseData();
            data.odatatype = dataType;
        }

        public void AddAction(string actionType)
        {
            this.data.actions = new List<Action>();
            this.data.actions.Add(new Action(actionType));
        }
    }

    public class ResponseData
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public List<Action> actions { get; set; }
    }

    public class Action
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Claims claims { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Inputs inputs { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AttributeError> attributeErrors { get; set; }

        public Action(string actionType)
        {
            odatatype = actionType;

            if (actionType == ActionType.ProvideClaimsForToken)
                claims = new Claims();
            else if (actionType == ActionType.SetPrefillValues)
                inputs = new Inputs();
            else if (actionType == ActionType.ShowValidationError)
                attributeErrors = new List<AttributeError>();
        }
    }

    public class Claims
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CorrelationId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string LoyaltyNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string LoyaltySince { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string LoyaltyTier { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ApiVersion { get; set; }

        public List<string> CustomRoles { get; set; }
        public Claims()
        {
            CustomRoles = new List<string>();
        }
    }

    public class Inputs
    {
        public string jobTitle { get; set; }
    }

    public class AttributeError
    {
        public string name { get; set; }
        public string value { get; set; }

        public AttributeError(string Name, string Value)
        {
            this.name = Name;
            this.value = Value;
        }
    }

    public class ResponseType
    {
        public const string OnTokenIssuanceStartResponseData = "microsoft.graph.onTokenIssuanceStartResponseData";
        public const string OnAttributeCollectionStartResponseData = "microsoft.graph.onAttributeCollectionStartResponseData";
        public const string OnAttributeCollectionSubmitResponseData = "microsoft.graph.onAttributeCollectionSubmitResponseData";
    }

    public class ActionType
    {
        public const string ProvideClaimsForToken = "microsoft.graph.provideClaimsForToken";
        public const string ShowValidationError = "microsoft.graph.ShowValidationError";
        public const string ContinueWithDefaultBehavior = "microsoft.graph.continueWithDefaultBehavior";
        public const string ModifyAttributeValues = "microsoft.graph.modifyAttributeValues";
        public const string ShowBlockPage = "microsoft.graph.showBlockPage";
        public const string SetPrefillValues = "microsoft.graph.setPrefillValues";

    }
}
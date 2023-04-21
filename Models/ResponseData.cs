using System.Text.Json.Serialization;

namespace woodgroveapi.Models.Response
{

    public class ResponseData
    {
        [JsonPropertyName("data")]
        public Data data { get; set; }
        public ResponseData()
        {
            data = new Data();
        }
    }

    public class Data
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public List<Action> actions { get; set; }
        public Data()
        {
            odatatype = "microsoft.graph.onTokenIssuanceStartResponseData";
            actions = new List<Action>();
            actions.Add(new Action());
        }
    }

    public class Action
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public Claims claims { get; set; }
        public Action()
        {
            odatatype = "microsoft.graph.provideClaimsForToken";
            claims = new Claims();
        }
    }

    public class Claims
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CorrelationId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string LoyaltyNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ApiVersion { get; set; }

        public List<string> CustomRoles { get; set; }
        public Claims()
        {
            CustomRoles = new List<string>();
        }
    }
}
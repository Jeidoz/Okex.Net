using Newtonsoft.Json;

namespace CustomOkexClient.RestObjects.Responses.Funding
{
    public class BaseResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
        
        [JsonIgnore]
        public bool IsSuccessStatusCode => Code == 0;
    }
    public class BaseResponse<T> : BaseResponse where T : class
    {
        [JsonProperty("data")]
        public T[] Data { get; set; }
    }
}
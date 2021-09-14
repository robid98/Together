using Newtonsoft.Json;

namespace Together.Data.DTOs
{
    public class UserLoginDTO
    {
        [JsonProperty(PropertyName = "userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty(PropertyName = "userPassword")]
        public string UserPassword { get; set; }
    }
}

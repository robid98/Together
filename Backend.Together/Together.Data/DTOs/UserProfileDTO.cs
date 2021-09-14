using Newtonsoft.Json;
using System;

namespace Together.Data.DTOs
{
    public class UserProfileDTO
    {
        [JsonProperty(PropertyName = "userProfileId")]
        public Guid UserProfileId { get; set; }

        [JsonProperty(PropertyName = "userProfileImage")]
        public byte[] UserProfileImg { get; set; }

        [JsonProperty(PropertyName = "userPostsNumber")]
        public int UserPostsNumber { get; set; }

        [JsonProperty(PropertyName = "userFriendsNumber")]
        public int UserFriendsNumber { get; set; }
    }
}

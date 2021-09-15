using Newtonsoft.Json;
using System;


namespace Together.Data.DTOs
{
    public class UserProfileDTO
    {
        [JsonProperty(PropertyName = "userprofileid")]
        public Guid UserProfileId { get; set; }

        [JsonProperty(PropertyName = "userprofileimggeneratedname")]
        public string UserProfileImgGeneratedName { get; set; }

        [JsonProperty(PropertyName = "birthdaydate")]
        public DateTime? BirthdayDate { get; set; }

        [JsonProperty(PropertyName = "profiledescription")]
        public string ProfileDescription { get; set; }

        [JsonProperty(PropertyName = "userpostsnumber")]
        public int? UserPostsNumber { get; set; }

        [JsonProperty(PropertyName = "userfriendsnumber")]
        public int? UserFriendsNumber { get; set; }
    }
}

using Newtonsoft.Json;
using System;

namespace Together.Data.DTOs
{
    public class PostDTO
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "postDescription")]
        public string PostDescription { get; set; }

        [JsonProperty(PropertyName = "postDate")]
        public DateTime? PostDate { get; set; }

        [JsonProperty(PropertyName = "postLikes")]
        public int? PostLikes { get; set; }

        [JsonProperty(PropertyName = "postShares")]
        public int? PostShares { get; set; }

        [JsonProperty(PropertyName = "postDeleted")]
        public string PostDeleted { get; set; }

        [JsonProperty(PropertyName = "userprofileid")]
        public Guid UserProfileId { get; set; }
    }
}

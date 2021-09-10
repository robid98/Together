using Newtonsoft.Json;
using System;

namespace Together.Data.DTOs
{
    public class CommentDTO
    {
        [JsonProperty(PropertyName = "commentId")]
        public Guid CommentId { get; set; }

        [JsonProperty(PropertyName = "commentDescription")]
        public string CommentDescription { get; set; }

        [JsonProperty(PropertyName = "commentDate")]
        public DateTime CommentDate { get; set; }

        [JsonProperty(PropertyName = "commentLikes")]
        public int CommentLikes { get; set; }

        [JsonProperty(PropertyName = "commentDeleted")]
        public string CommentDeleted { get; set; }
    }
}

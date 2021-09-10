using Newtonsoft.Json;
using System;

namespace Together.Data.DTOs
{
    public class ReplyDTO
    {
        [JsonProperty(PropertyName = "replyId")]
        public Guid ReplyId { get; set; }

        [JsonProperty(PropertyName = "replyDescription")]
        public string ReplyDescription { get; set; }

        [JsonProperty(PropertyName = "replyDate")]
        public DateTime ReplyDate { get; set; }

        [JsonProperty(PropertyName = "replyLikes")]
        public int ReplyLikes { get; set; }

        [JsonProperty(PropertyName = "replyDeleted")]
        public string IsReplyDeleted { get; set; }
    }
}

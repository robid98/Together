using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class ReplyModel
    {
        [JsonProperty(PropertyName = "replyId")]
        [Key]
        public Guid ReplyId { get; set; }

        [JsonProperty(PropertyName = "replyDescription")]
        public string ReplyDescription { get; set; }

        [JsonProperty(PropertyName = "replyDate")]
        public DateTime ReplyDate { get; set; }

        [JsonProperty(PropertyName = "replyLikes")]
        public int ReplyLikes { get; set; }

        [JsonProperty(PropertyName = "replyIsDeleted")]
        public bool IsReplyDeleted { get; set; }

        [JsonProperty(PropertyName = "commentId")]
        /* Navigation propriety - One to Many relationship between Comment and Replies */
        public Guid CommentId { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public CommentModel Comment { get; set; }

    }
}

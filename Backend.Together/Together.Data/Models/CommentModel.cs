using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class CommentModel
    {
        [JsonProperty(PropertyName = "commentId")]
        [Key]
        public Guid CommentId { get; set; }

        [JsonProperty(PropertyName = "commentDescription")]
        public string CommentDescription { get; set; }

        [JsonProperty(PropertyName = "commentDate")]
        public DateTime CommentDate { get; set; }

        [JsonProperty(PropertyName = "commentLikes")]
        public int CommentLikes { get; set; }

        [JsonProperty(PropertyName = "isCommentDeleted")]
        public bool IsCommentDeleted { get; set; }

        /* Navigation Proprieties  - One to many relationship between Posts and Comments */
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "post")]
        public PostModel Post { get; set; }

        [JsonProperty(PropertyName = "commentReplies")]
        public List<ReplyModel> CommentReplies { get; set; }
    }
}

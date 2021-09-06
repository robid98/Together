using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class PostModel
    {
        [JsonProperty(PropertyName = "postId")]
        [Key]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "postDescription")]
        public string PostDescription { get; set; }

        [JsonProperty(PropertyName = "postDate")]
        public DateTime PostDate { get; set; }

        [JsonProperty(PropertyName = "postLikes")]
        public int PostLikes { get; set; }

        [JsonProperty(PropertyName = "postShares")]
        public int PostShares { get; set; }

        [JsonProperty(PropertyName = "postIsDeleted")]
        public bool IsPostDeleted { get; set; }

        [JsonProperty(PropertyName = "postComments")]
        /* Navigation Propriety - One to many relationship between Posts and Comments */
        public List<CommentModel> PostComments { get; set; }

    }
}

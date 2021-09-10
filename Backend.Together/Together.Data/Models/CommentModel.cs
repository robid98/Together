using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class CommentModel
    {
        [Key]
        public Guid CommentId { get; set; }

        public string CommentDescription { get; set; }

        public DateTime CommentDate { get; set; }

        public int CommentLikes { get; set; }

        public string CommentDeleted { get; set; }

        /* Navigation Proprieties  - One to many relationship between Posts and Comments */
        public Guid PostId { get; set; }

        public PostModel Post { get; set; }

        public List<ReplyModel> CommentReplies { get; set; }
    }
}

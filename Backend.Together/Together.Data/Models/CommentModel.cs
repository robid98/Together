using System;
using System.Collections.Generic;

namespace Together.Data.Models
{
    public class CommentModel
    {
        public Guid CommentGuid { get; set; }

        public string CommentDescription { get; set; }
        
        public DateTime CommentDate { get; set; }

        public int CommentLikes { get; set; }

        public bool IsCommentDeleted { get; set; }

        /* Navigation Proprieties  - One to many relationship between Posts and Comments */
        public Guid PostId { get; set; }

        public PostModel Post { get; set; }

        public List<ReplyModel> CommentReplies { get; set; }
    }
}

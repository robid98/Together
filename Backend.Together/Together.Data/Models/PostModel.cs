using System;
using System.Collections.Generic;

namespace Together.Data.Models
{
    public class PostModel
    {
        public Guid PostId { get; set; }

        public string PostDescription { get; set; }

        public DateTime PostDate { get; set; }

        public int PostLikes { get; set; }

        public int PostShares { get; set; }

        public bool IsPostDeleted { get; set; }

        /* Navigation Propriety - One to many relationship between Posts and Comments */
        public List<CommentModel> PostComments { get; set; }

    }
}

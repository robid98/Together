using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class ReplyModel
    {
        [Key]
        public Guid ReplyId { get; set; }

        public string ReplyDescription { get; set; }

        public DateTime ReplyDate { get; set; }

        public int ReplyLikes { get; set; }

        public bool IsReplyDeleted { get; set; }

        /* Navigation propriety - One to Many relationship between Comment and Replies */
        public Guid CommentId { get; set; }

        public CommentModel Comment { get; set; }

    }
}

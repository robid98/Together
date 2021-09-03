using System;

namespace Together.Data.Models
{
    public class PostModel
    {
        public Guid PostId { get; set; }

        public string PostDescription { get; set; }

        public DateTime PostDate { get; set; }

        public int PostLikes { get; set; }

        public int PostShares { get; set; }

    }
}

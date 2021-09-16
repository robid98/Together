using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class UserProfileModel
    {
        [Key]
        public Guid UserProfileId { get; set; }

        public string UserProfileImgBlobLink{ get; set; }

        public DateTime? BirthdayDate { get; set; }

        public string ProfileDescription { get; set; }

        public int? UserPostsNumber { get; set; }

        public int? UserFriendsNumber { get; set; }

        /* Navigation Proprieties */
        public Guid UserId { get; set; }
        public UserAuthenticationModel UserAuthenticationModel { get; set; }
        public List<PostModel> UserPosts { get; set; }
    }
}

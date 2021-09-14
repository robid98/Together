using System;
using System.ComponentModel.DataAnnotations;

namespace Together.Data.Models
{
    public class UserAuthenticationModel
    {
        [Key]
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Password { get; set; }

        /* Navigation Propriety */
        public UserProfileModel UserProfileModel { get; set; }
    }
}

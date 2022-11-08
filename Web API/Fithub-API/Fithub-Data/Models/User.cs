using System.ComponentModel.DataAnnotations;

namespace Fithub_Data.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string ExternalLoginProvider { get; set; }
        public string ExternalLoginProviderName { get; set; }
        public string ExternalProviderKey { get; set; }
        public bool IsExternalProvider { get; set; }
        public bool IsActive { get; set; }

        public UserRole Role { get; set; }

        public UserInfo UserInfo { get; set; }

    }
}

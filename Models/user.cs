using System.ComponentModel.DataAnnotations;

namespace AlhalabiShopping.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
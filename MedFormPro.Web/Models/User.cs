using System.ComponentModel.DataAnnotations;

namespace MedFormPro.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }
    }
}
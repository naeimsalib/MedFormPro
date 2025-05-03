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
        public required string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Pharmacist,
        ReviewTeam,
        DeliveryTeam
    }
}
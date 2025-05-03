using System.ComponentModel.DataAnnotations;

namespace MedFormPro.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Pharmacist,
        ReviewTeam,
        DeliveryTeam
    }
}
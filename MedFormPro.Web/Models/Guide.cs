using System.ComponentModel.DataAnnotations;

namespace MedFormPro.Web.Models
{
    public class Guide
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public required string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public int DisplayOrder { get; set; }

        [StringLength(50)]
        public string? Icon { get; set; }

        public string? RoleAccess { get; set; }
    }
}
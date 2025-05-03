using System.ComponentModel.DataAnnotations;

namespace MedFormPro.Web.Models
{
    public class Guide
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        public int DisplayOrder { get; set; }

        [StringLength(50)]
        public string? Icon { get; set; }

        public string? RoleAccess { get; set; }
    }
}
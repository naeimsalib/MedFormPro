using System.ComponentModel.DataAnnotations;

namespace MedFormPro.Web.Models
{
    public class CreatePrescriptionViewModel
    {
        [Required(ErrorMessage = "Patient name is required")]
        [Display(Name = "Patient Name")]
        [StringLength(100, ErrorMessage = "Patient name cannot exceed 100 characters")]
        public string? PatientName { get; set; }

        [Required(ErrorMessage = "Medication name is required")]
        [Display(Name = "Medication Name")]
        [StringLength(100, ErrorMessage = "Medication name cannot exceed 100 characters")]
        public string? MedicationName { get; set; }

        [Required(ErrorMessage = "Dosage is required")]
        [StringLength(50, ErrorMessage = "Dosage cannot exceed 50 characters")]
        public string? Dosage { get; set; }

        [Required(ErrorMessage = "Instructions are required")]
        [StringLength(500, ErrorMessage = "Instructions cannot exceed 500 characters")]
        public string? Instructions { get; set; }
    }

    public class PrescriptionDetailsViewModel
    {
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? MedicationName { get; set; }
        public string? Dosage { get; set; }
        public string? Instructions { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public PrescriptionStatus Status { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public string? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewNotes { get; set; }
    }

    public class ReviewPrescriptionViewModel
    {
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? MedicationName { get; set; }
        public string? Dosage { get; set; }
        public string? Instructions { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        [Required(ErrorMessage = "Review decision is required")]
        [Display(Name = "Review Decision")]
        public PrescriptionStatus Status { get; set; }

        [Required(ErrorMessage = "Review notes are required")]
        [Display(Name = "Review Notes")]
        [StringLength(500, ErrorMessage = "Review notes cannot exceed 500 characters")]
        public string? ReviewNotes { get; set; }
    }
}
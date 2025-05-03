using System;
using System.ComponentModel.DataAnnotations;

namespace MedFormPro.Web.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        [StringLength(100, ErrorMessage = "Patient name cannot exceed 100 characters")]
        public string? PatientName { get; set; }

        [Required(ErrorMessage = "Medication name is required")]
        [StringLength(100, ErrorMessage = "Medication name cannot exceed 100 characters")]
        public string? MedicationName { get; set; }

        [Required(ErrorMessage = "Dosage is required")]
        [StringLength(50, ErrorMessage = "Dosage cannot exceed 50 characters")]
        public string? Dosage { get; set; }

        [Required(ErrorMessage = "Instructions are required")]
        [StringLength(500, ErrorMessage = "Instructions cannot exceed 500 characters")]
        public string? Instructions { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Creator information is required")]
        public string? CreatedBy { get; set; }

        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Submitted;

        public ShipmentStatus ShipmentStatus { get; set; } = ShipmentStatus.Pending;

        public string? ReviewedBy { get; set; }

        public DateTime? ReviewedDate { get; set; }

        public string? ReviewNotes { get; set; }
    }

    public enum PrescriptionStatus
    {
        Submitted,
        Approved,
        Denied
    }

    public enum ShipmentStatus
    {
        Pending,
        Backorder,
        Fulfilled,
        Shipped,
        Delivered
    }
}
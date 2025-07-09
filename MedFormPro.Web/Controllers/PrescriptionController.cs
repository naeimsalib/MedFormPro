using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MedFormPro.Web.Data;
using MedFormPro.Web.Models;

namespace MedFormPro.Web.Controllers
{
    [Authorize]
    public class PrescriptionController : BaseController
    {
        public PrescriptionController(ApplicationDbContext context) : base(context) { }

        // GET: Prescription
        public async Task<IActionResult> Index()
        {
            var prescriptions = await _context.Prescriptions
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            // Filter prescriptions based on user role
            if (User.Identity?.Name != null)
            {
                if (User.IsInRole("Pharmacist"))
                {
                    prescriptions = prescriptions.Where(p => p.CreatedBy == User.Identity.Name).ToList();
                }
                else if (User.IsInRole("ReviewTeam"))
                {
                    prescriptions = prescriptions.Where(p => p.Status == PrescriptionStatus.Submitted).ToList();
                }
                else if (User.IsInRole("DeliveryTeam"))
                {
                    prescriptions = prescriptions.Where(p => p.Status == PrescriptionStatus.Approved).ToList();
                }
            }

            return View(prescriptions.Select(p => new PrescriptionDetailsViewModel
            {
                Id = p.Id,
                PatientName = p.PatientName ?? string.Empty,
                MedicationName = p.MedicationName ?? string.Empty,
                Dosage = p.Dosage ?? string.Empty,
                Instructions = p.Instructions ?? string.Empty,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy ?? string.Empty,
                Status = p.Status,
                ShipmentStatus = p.ShipmentStatus,
                ReviewedBy = p.ReviewedBy ?? string.Empty,
                ReviewedDate = p.ReviewedDate,
                ReviewNotes = p.ReviewNotes ?? string.Empty
            }));
        }

        // GET: Prescription/Create
        [Authorize(Policy = "RequirePharmacistRole")]
        public IActionResult Create()
        {
            return View(new CreatePrescriptionViewModel());
        }

        // POST: Prescription/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequirePharmacistRole")]
        public async Task<IActionResult> Create(CreatePrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prescription = new Prescription
                {
                    PatientName = model.PatientName,
                    MedicationName = model.MedicationName,
                    Dosage = model.Dosage,
                    Instructions = model.Instructions,
                    CreatedBy = User.Identity?.Name ?? "Unknown",
                    CreatedDate = DateTime.UtcNow,
                    Status = PrescriptionStatus.Submitted,
                    ShipmentStatus = ShipmentStatus.Pending,
                    ReviewNotes = string.Empty,
                    ReviewedBy = string.Empty
                };

                _context.Add(prescription);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Prescription created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Prescription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(new PrescriptionDetailsViewModel
            {
                Id = prescription.Id,
                PatientName = prescription.PatientName ?? string.Empty,
                MedicationName = prescription.MedicationName ?? string.Empty,
                Dosage = prescription.Dosage ?? string.Empty,
                Instructions = prescription.Instructions ?? string.Empty,
                CreatedDate = prescription.CreatedDate,
                CreatedBy = prescription.CreatedBy ?? string.Empty,
                Status = prescription.Status,
                ShipmentStatus = prescription.ShipmentStatus,
                ReviewedBy = prescription.ReviewedBy ?? string.Empty,
                ReviewedDate = prescription.ReviewedDate,
                ReviewNotes = prescription.ReviewNotes ?? string.Empty
            });
        }

        // GET: Prescription/Review/5
        [Authorize(Policy = "RequireReviewTeamRole")]
        public async Task<IActionResult> Review(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(p => p.Id == id && p.Status == PrescriptionStatus.Submitted);

            if (prescription == null)
            {
                return NotFound();
            }

            return View(new ReviewPrescriptionViewModel
            {
                Id = prescription.Id,
                PatientName = prescription.PatientName ?? string.Empty,
                MedicationName = prescription.MedicationName ?? string.Empty,
                Dosage = prescription.Dosage ?? string.Empty,
                Instructions = prescription.Instructions ?? string.Empty,
                CreatedDate = prescription.CreatedDate,
                CreatedBy = prescription.CreatedBy ?? string.Empty
            });
        }

        // POST: Prescription/Review/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireReviewTeamRole")]
        public async Task<IActionResult> Review(int id, ReviewPrescriptionViewModel model)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null || prescription.Status != PrescriptionStatus.Submitted)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                prescription.Status = model.Status;
                prescription.ReviewedBy = User.Identity?.Name ?? "Unknown";
                prescription.ReviewedDate = DateTime.UtcNow;
                prescription.ReviewNotes = model.ReviewNotes ?? string.Empty;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Prescription reviewed successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Prescription/UpdateShipment/5
        [Authorize(Policy = "RequireDeliveryTeamRole")]
        public async Task<IActionResult> UpdateShipment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(p => p.Id == id && p.Status == PrescriptionStatus.Approved);

            if (prescription == null)
            {
                return NotFound();
            }

            return View(new PrescriptionDetailsViewModel
            {
                Id = prescription.Id,
                PatientName = prescription.PatientName ?? string.Empty,
                MedicationName = prescription.MedicationName ?? string.Empty,
                Dosage = prescription.Dosage ?? string.Empty,
                ShipmentStatus = prescription.ShipmentStatus
            });
        }

        // POST: Prescription/UpdateShipment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireDeliveryTeamRole")]
        public async Task<IActionResult> UpdateShipment(int id, ShipmentStatus status)
        {
            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(p => p.Id == id && p.Status == PrescriptionStatus.Approved);

            if (prescription == null)
            {
                return NotFound();
            }

            prescription.ShipmentStatus = status;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Shipment status updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Prescription/ReviewHistory
        [Authorize(Policy = "RequireReviewTeamRole")]
        public async Task<IActionResult> ReviewHistory()
        {
            if (User.Identity?.Name == null)
            {
                return RedirectToAction("Index");
            }

            var reviewedPrescriptions = await _context.Prescriptions
                .Where(p => p.ReviewedBy == User.Identity.Name)
                .OrderByDescending(p => p.ReviewedDate)
                .ToListAsync();

            return View(reviewedPrescriptions.Select(p => new PrescriptionDetailsViewModel
            {
                Id = p.Id,
                PatientName = p.PatientName ?? string.Empty,
                MedicationName = p.MedicationName ?? string.Empty,
                Dosage = p.Dosage ?? string.Empty,
                Instructions = p.Instructions ?? string.Empty,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy ?? string.Empty,
                Status = p.Status,
                ShipmentStatus = p.ShipmentStatus,
                ReviewedBy = p.ReviewedBy ?? string.Empty,
                ReviewedDate = p.ReviewedDate,
                ReviewNotes = p.ReviewNotes ?? string.Empty
            }));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedFormPro.Web.Data;
using MedFormPro.Web.Models;

namespace MedFormPro.Web.Controllers
{
    public class GuideController : BaseController
    {
        public GuideController(ApplicationDbContext context) : base(context) { }

        // GET: Guide
        public async Task<IActionResult> Index()
        {
            var guides = await _context.Guides
                .OrderBy(g => g.Category)
                .ThenBy(g => g.DisplayOrder)
                .ToListAsync();
            return View(guides);
        }

        // GET: Guide/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guide = await _context.Guides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guide == null)
            {
                return NotFound();
            }

            return View(guide);
        }

        // GET: Guide/GetGuideContent/5
        public async Task<IActionResult> GetGuideContent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guide = await _context.Guides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guide == null)
            {
                return NotFound();
            }

            return Json(new { title = guide.Title, content = guide.Content });
        }

        // GET: Guide/Category/RoleGuide
        public async Task<IActionResult> Category(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var guides = await _context.Guides
                .Where(g => g.Category == id)
                .OrderBy(g => g.DisplayOrder)
                .ToListAsync();

            return View(guides);
        }
    }
}
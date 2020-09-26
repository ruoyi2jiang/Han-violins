
using System.Linq;
using System.Threading.Tasks;
using HansViolinWebApp.Data;
using HansViolinWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HansViolinWebApp.Controllers
{
    //[Route("[controller]")]
    public class InstrumentsController : Controller
    {

        private readonly HansViolinWebContext _context;

        public InstrumentsController(HansViolinWebContext context)
        {
            _context = context;
        }

        [HttpGet("instruments/{pathName}")]
        public async Task<IActionResult> Categorize(string pathName)
        {
            var category = await _context.Categories.Where(c => c.PathName == pathName).FirstOrDefaultAsync();
            if(category == null)
            {
                return NotFound();
            }
            ViewData["Title"] = category.CategoryName;
            ViewData["Category"] = category;
            var item = await _context.Items
                .Where(c => c.CategoryName == pathName
                       && c.Status == Status.ACTIVE.ToString())
                .OrderByDescending(i => i.ItemId)
                .Include(i => i.Images).ToListAsync();

            return View("InstrumentList", item);
        }

        [HttpGet("instruments/detail/{id?}")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Items
                .Where(i => i.ItemId == id)
                .Include(c=>c.Category)
                .Include(i => i.Images)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }
            ViewData["Category"] = item.Category.CategoryName;

            return View("InstrumentDetail", item);
        }

        [Route("/notable-sales")]
        public async Task<IActionResult> NotableSales()
        {
            var categories = await _context.Categories.ToListAsync();
            ViewData["Categories"] = categories;
            var items = await _context.Items
                .Include(c => c.Category)
                .Include(i => i.Images)
                .Where(i => i.Status == Status.NOTABLE_SALE.ToString())
                .OrderByDescending(i => i.ItemId)
                .ToListAsync();
            return View(items);
        }
    }
}
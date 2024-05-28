
using System.Linq;
using System.Threading.Tasks;
using HansViolinWebApp.Data;
using HansViolinWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Localization;
using HansViolinWebApp.Models.Mapper;

namespace HansViolinWebApp.Controllers
{
    //[Route("[controller]")]
    public class InstrumentsController : Controller
    {

        private readonly HansViolinWebContext _context;
        private CategoryMapper categoryMapper = new CategoryMapper();
        private ItemMapper itemMapper = new ItemMapper();

        public InstrumentsController(HansViolinWebContext context)
        {
            _context = context;
        }

        [HttpGet("instruments/{pathName}")]
        public async Task<IActionResult> Categorize(string pathName)
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();

            var category = await _context.Categories.Where(c => c.PathName == pathName).FirstOrDefaultAsync();
            if(category == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Where(c => c.CategoryName == pathName
                       && c.Status == Status.ACTIVE.ToString())
                .OrderByDescending(i => i.ItemId)
                .Include(i => i.Images).ToListAsync();
            if(requestCulture.RequestCulture.Culture.Name == "zh")
            {
                var categoryModel = categoryMapper.map(category, "zh");
                ViewData["Title"] = category.CategoryNameZh;
                ViewData["Category"] = categoryModel;
            }
            else
            {
                ViewData["Title"] = category.CategoryName;
                ViewData["Category"] = category;
            }
            return View("InstrumentList", item);
        }

        [HttpGet("instruments/detail/{id?}")]
        public async Task<IActionResult> Detail(int? id)
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();

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
            ViewData["Images"] = item.Images;

            if (requestCulture.RequestCulture.Culture.Name == "zh")
            {
                ViewData["Category"] = item.Category.CategoryNameZh;
                var itemModel = itemMapper.map(item, "zh");
                return View("InstrumentDetail", itemModel);

            }
            else
            {
                ViewData["Category"] = item.Category.CategoryName;
                var itemModel = itemMapper.map(item, "en");
                return View("InstrumentDetail", itemModel);

            }
        }

        [Route("/notable-sales")]
        public async Task<IActionResult> NotableSales()
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();

            var categories = await _context.Categories.ToListAsync();

            var items = await _context.Items
                .Include(c => c.Category)
                .Include(i => i.Images)
                .Where(i => i.Status == Status.NOTABLE_SALE.ToString())
                .OrderByDescending(i => i.ItemId)
                .ToListAsync();

            if (requestCulture.RequestCulture.Culture.Name == "zh")
            {
                var categoryModels = categoryMapper.map(categories, "zh");
                ViewData["Categories"] = categoryModels;
            }
            else
            {
                ViewData["Categories"] = categories;

            }

            return View(items);
        }
    }
}
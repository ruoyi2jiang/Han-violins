using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using HansViolinWebApp.Data;

namespace HansViolinWebApp.ViewComponents
{
    public class ItemListViewComponent: ViewComponent
    {
        private readonly HansViolinWebContext _context;

        public ItemListViewComponent(HansViolinWebContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            ViewData["Category"] = categoryId;
            return View(await _context.Items.Where(c => c.CategoryId == categoryId).Include(i=>i.Images).ToListAsync());
        }
    }
}

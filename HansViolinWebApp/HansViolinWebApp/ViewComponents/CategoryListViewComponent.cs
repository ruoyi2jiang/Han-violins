using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HansViolinWebApp.Data;

namespace HansViolinWebApp.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly HansViolinWebContext _context;

        public CategoryListViewComponent(HansViolinWebContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}

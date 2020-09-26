using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using Microsoft.EntityFrameworkCore;
using HansViolinWebApp.Data;

namespace HansViolinWebApp.ViewComponents
{
    public class DropDownMenuViewComponent : ViewComponent
    {
        private readonly HansViolinWebContext _context;

        public DropDownMenuViewComponent(HansViolinWebContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }

}

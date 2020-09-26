using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HansViolinWebApp.ViewComponents
{
    public class SocialLinksViewComponent : ViewComponent
    {
        private readonly HansViolinWebContext _context;

        public SocialLinksViewComponent(HansViolinWebContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.BusinessDetails.FirstOrDefaultAsync());
        }
    }
}

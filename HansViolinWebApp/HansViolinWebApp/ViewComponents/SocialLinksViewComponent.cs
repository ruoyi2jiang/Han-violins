using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using Microsoft.EntityFrameworkCore;
using HansViolinWebApp.Data;

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
            var profile = await _context.BusinessDetails.FirstOrDefaultAsync();
            if(profile == null)
            {
                profile = new BusinessDetail();
            }
            return View(profile);
        }
    }
}

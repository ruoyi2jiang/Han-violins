using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HansViolinWebApp.ViewComponents
{
    public class BusinessProfileViewComponent : ViewComponent
    {
        private readonly HansViolinWebContext _context;

        public BusinessProfileViewComponent(HansViolinWebContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var profile = await _context.BusinessDetails.FirstOrDefaultAsync();
            if(profile == null)
            {
                profile = new BusinessDetail();
                _context.BusinessDetails.Add(profile);
                await _context.SaveChangesAsync();
            }
            return View(profile);
        }
    }
}

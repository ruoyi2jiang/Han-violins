using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using HansViolinWebApp.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HansViolinWebApp.ViewComponents
{
    public class NewsManagerViewComponent :ViewComponent
    {
        private NewsRepository newsRepository;
        public NewsManagerViewComponent (HansViolinWebContext context)
        {
            newsRepository = new NewsRepository(context);
        }

        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync(bool isPublished)
        {
            if (isPublished)
            {
                return View(await newsRepository.GetAllPublishedNews());
            }
            else
            {
                return View(await newsRepository.GetAllUnPublishedNews());
            } 
        }
    }
}

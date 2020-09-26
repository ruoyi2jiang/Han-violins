using System;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using HansViolinWebApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HansViolinWebApp.Controllers
{

    [Route("/news")]
    public class NewsController : Controller
    {
        private NewsRepository newsRepository;
        public NewsController(HansViolinWebContext context)
        {
            newsRepository = new NewsRepository(context);
        }
        public async Task<IActionResult> Index()
        {
            var newsList = await newsRepository.GetAllPublishedNews();
            return View(newsList);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetNews(int? id)
        {
            News news;
            try
            {
                news = await newsRepository.GetNews(id);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            if(news == null)
            {
                return NotFound();
            }
            return View(news);
        }
    }
}
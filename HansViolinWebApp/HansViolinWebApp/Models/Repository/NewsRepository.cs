using HansViolinWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HansViolinWebApp.Models.Repository
{
    public class NewsRepository
    {
        private readonly HansViolinWebContext _context;

        public NewsRepository(HansViolinWebContext context)
        {
            _context = context;
        }

        public async Task<List<News>> GetAllNews()
        {
            return await _context.News.OrderByDescending(n => n.PublishedDate).ToListAsync();
        }

        public async Task<List<News>> GetAllPublishedNews()
        {
            return await _context.News.Where(n => n.IsPublished == true).OrderByDescending(n => n.PublishedDate).ToListAsync();
        }

        public async Task<List<News>> GetAllUnPublishedNews()
        {
            return await _context.News.Where(n => n.IsPublished == false).OrderByDescending(n => n.CreatedDate).ToListAsync();
        }

        public async Task<News> GetNews(int? id)
        {
            if(id == null)
            {
                throw new NullReferenceException("Id cannot be null");
            }
            return await _context.News.FindAsync(id);
        }

        public async Task AddNews(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNews(News news)
        {
            try
            {
                _context.News.Update(news);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(news.Id))
                {
                    throw new NullReferenceException("News Id is not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(n => n.Id == id);
        }
    }
}

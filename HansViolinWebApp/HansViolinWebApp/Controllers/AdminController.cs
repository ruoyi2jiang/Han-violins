using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using HansViolinWebApp.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HansViolinWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly HansViolinWebContext _context;
        private NewsRepository newsRepository;

        public AdminController(HansViolinWebContext context)
        {
            _context = context;
            newsRepository = new NewsRepository(_context);
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Item Controller
        public IActionResult ItemList(int? categoryId)
        {
            return ViewComponent("ItemList", categoryId);
        }

        [HttpGet]
        public IActionResult CreateItem(int? categoryId)
        {
            var item = new Item();

            if(categoryId == null)
            {
                return NotFound();
            }

            item.CategoryId = categoryId;
            item.Category = _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            if (item.Category == null)
            {
                return NotFound();
            }

            item.CategoryName = item.Category.PathName;
            AddStatusViewData();
            return PartialView("_CreateItem", item);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem([Bind("ItemId,CategoryId,CategoryName,ItemName,Description,PriceRange,CoverId,OriginInfo,Status")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.DateAdded = DateTime.UtcNow;
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateItem", item);
        }

        public async Task<IActionResult> EditItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            AddStatusViewData();
            return PartialView("_EditItem", item);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(int id, [Bind("ItemId,CategoryId,CategoryName,ItemName,Description,PriceRange,CoverId,OriginInfo,Status,DateAdded")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_EditItem", item);
        }

        public async Task<IActionResult> EditItemCover(int? id, int? coverId)
        {
            if (id == null || coverId == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            var image = await _context.Images.FindAsync(coverId);
            if (item == null || image == null)
            {
                return NotFound();
            }

            item.CoverId = coverId;
            return PartialView("_EditItemCover", item);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItemCover(int id, [Bind("ItemId,CategoryId,CategoryName,ItemName,Description,PriceRange,CoverId,OriginInfo,Status,DateAdded")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_EditItemCover", item);
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteItem", item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("DeleteItem")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItemConfirmed(int id)
        {
            //remove all its images
            var images = await _context.Images.Where(i => i.ItemId == id).ToListAsync();
            _context.Images.RemoveRange(images);

            //remove the item
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return PartialView("_DeleteItem", item);
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
        #endregion

        #region Category Controller

        public IActionResult CategoryList()
        {
            return ViewComponent("CategoryList");
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            var category = new Category();
            return PartialView("_CreateCategory", category);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("CategoryName,PathName,Description,CoverUrl")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateCategory", category);
        }

        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_EditCategory", category);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [Bind("CategoryId,CategoryName,PathName,Description,CoverUrl")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_EditCategory", category);
        }
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteCategory", category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("DeleteCategory")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(int id)
        {
            // remove items under the category first
            var items = await _context.Items.Where(i => i.CategoryId == id).ToListAsync();
            _context.Items.RemoveRange(items);

            //remove the category
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
        #endregion

        #region Image Controller
        [HttpGet]
        public IActionResult CreateImage(int? itemId)
        {
            var image = new Image();
            if(itemId == null)
            {
                return NotFound();
            }
            var item = _context.Items.Where(i => i.ItemId == itemId).FirstOrDefault();
            if(item == null)
            {
                return NotFound();
            }
            image.ItemId = itemId;
            image.Item = item;
            image.SequenceNumber = _context.Images.Where(i => i.ItemId == itemId).Count();
            
            return PartialView("_CreateImage", image);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImage([Bind("ImageId,ItemId,Url,SequenceNumber")] Image image)
        {
            if (ModelState.IsValid)
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateImage", image);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteImage", image);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("DeleteImage")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImageConfirmed(int id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Business Profile Controller

        public async Task<IActionResult> EditProfile()
        {
            var profile = await _context.BusinessDetails.FirstOrDefaultAsync();
            if (profile == null)
            {
                return NotFound();
            }
            return PartialView("_EditProfile", profile);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, [Bind("Id,About,ImageLink,Phone,Email,Address,Postcode,Twitter,Facebook,Instagram")] BusinessDetail profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_EditProfile", profile);
        }
        public async Task<IActionResult> EditContact()
        {
            var profile = await _context.BusinessDetails.FirstOrDefaultAsync();
            if (profile == null)
            {
                return NotFound();
            }
            return PartialView("_EditContact", profile);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(int id, [Bind("Id,About,ImageLink,Phone,Email,Address,Postcode,Twitter,Facebook,Instagram")] BusinessDetail profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_EditContact", profile);
        }


        private bool ProfileExists(int id)
        {
            return _context.BusinessDetails.Any(b => b.Id == id);
        }

        #endregion

        #region News Controller

        public async Task<IActionResult> NewsPreview(int? id)
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
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
        public IActionResult CreatePost()
        {
            var news = new News();
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("Id,Title,Content,CreatedDate,PublishedDate,IsPublished")] News news)
        {
            if (ModelState.IsValid)
            {
                news.CreatedDate = DateTime.UtcNow;
                news.IsPublished = false;
                await newsRepository.AddNews(news);
                return RedirectToAction(nameof(NewsPreview), new {id = news.Id });
            }
            return View(news);
        }

        public async Task<IActionResult> EditPost(int? id)
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
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,Title,Content,CreatedDate,PublishedDate,IsPublished")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await newsRepository.UpdateNews(news);
                }
                catch (NullReferenceException)
                {
                    return NotFound();
                }
                catch (Exception)
                {
                    return Forbid();
                }
                return RedirectToAction(nameof(NewsPreview), new { id = news.Id });
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePostStatus(int id, [Bind("Id,Title,Content,CreatedDate,PublishedDate,IsPublished")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    news.IsPublished = !news.IsPublished;
                    news.PublishedDate = DateTime.UtcNow;
                    await newsRepository.UpdateNews(news);
                }
                catch (NullReferenceException)
                {
                    return NotFound();
                }
                catch (Exception)
                {
                    return Forbid();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        public async Task<IActionResult> DeletePost(int? id)
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
            if (news == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePost", news);
        }

        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePostConfirmed(int id)
        {
            await newsRepository.DeleteNews(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
        public void AddStatusViewData()
        {
            var statusList = new List<Status>();
            statusList.Add(Status.DRAFT);
            statusList.Add(Status.ACTIVE);
            statusList.Add(Status.NOTABLE_SALE);
            ViewData["Status"] = new SelectList(statusList);
        }
    }
}
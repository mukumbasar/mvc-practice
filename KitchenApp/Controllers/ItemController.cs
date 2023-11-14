using KitchenApp.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KitchenApp.Models;

namespace KitchenApp.Controllers
{
    
    public class ItemController : Controller
    {
        public AppDbContext AppDbContext { get; set; }

        public ItemController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        
        public IActionResult Add()
        {
            ViewBag.List = AppDbContext.RecipeItems.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(RecipeItem item)
        {
            AppDbContext.RecipeItems.Add(item);
            AppDbContext.SaveChanges();

            return RedirectToAction("Add");
        }
        [Authorize]
        public IActionResult Delete(int id) 
        {
            var item = AppDbContext.RecipeItems.Find(id);
            AppDbContext.RecipeItems.Remove(item);
            AppDbContext.SaveChanges();

            return RedirectToAction("Add");
        }
        [Authorize]
        public IActionResult Update(int id)
        {
            var item = AppDbContext.RecipeItems.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(RecipeItem item)
        {
            AppDbContext.RecipeItems.Update(item);
            AppDbContext.SaveChanges();

            return RedirectToAction("Add");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ZayShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sport_Shop.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ZayShopContext db = new ZayShopContext();
        public IActionResult Index()
        {
            
            return View(db.Categories.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreateAt = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            var items = db.Categories.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {

                category.CreateAt = DateTime.Now;
                db.Categories.Update(category);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
    }
}

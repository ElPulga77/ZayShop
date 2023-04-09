using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {

        ZayShopContext db = new ZayShopContext();
        private IWebHostEnvironment _environment;
        public BannerController(ZayShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;

            _environment = webHostEnvironment;

        }
        public IActionResult Index()
        {         
            return View(db.Banners.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Banner sp, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _environment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\banner");
                    var extention = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);

                    }
                    sp.ImagePath = fileName + extention;
                }
                db.Banners.Add(sp);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(sp);
        }
    }
}

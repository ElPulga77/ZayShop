using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanphamController : Controller
    {
        ZayShopContext db = new ZayShopContext();
        private IWebHostEnvironment _environment;
        public SanphamController(ZayShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;

            _environment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public IActionResult Add()
        {
           
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product sanpham, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _environment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\sanpham");
                    var extention = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    sanpham.Image = fileName + extention;

                }

                db.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View(sanpham);
        }
    }
}

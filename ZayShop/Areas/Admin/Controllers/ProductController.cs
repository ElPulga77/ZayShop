using DoAnCF.Helpper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ZayShopContext db = new ZayShopContext();
        private IWebHostEnvironment _environment;
        public ProductController(ZayShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;

            _environment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            
            var dsSanPham = db.ProductDetails.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            return View(dsSanPham.ToList());
        }
        public IActionResult Add()
        {
            ViewBag.DSM = new SelectList(db.Colors.ToList(), "ColorId", "ColorName");
            ViewBag.DSS = new SelectList(db.Sizes.ToList(), "SizeId", "SizeName");
            ViewBag.DSSP = new SelectList(db.Products.ToList(), "ProductId", "ProductName");
            return View();
        }

        /*public IActionResult Add(ProductDetail sp, List<IFormFile >  file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _environment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\product");
                    var extention = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);

                    }
                    sp.Image = fileName + extention;
                }
                db.ProductDetails.Add(sp);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }       
            ViewBag.DSM = new SelectList(db.Colors.ToList(), "ColorId", "ColorName");
            ViewBag.DSS = new SelectList(db.Sizes.ToList(), "SizeId", "SizeName");
            ViewBag.DSSP = new SelectList(db.Products.ToList(), "ProductId", "ProductName");
            return View(sp);
        }*/
        /*public IActionResult Add(ProductDetail sp, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _environment.WebRootPath;

                if (file != null && file.Count >= 1)
                {
                    foreach (var f in file)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"image\product2");
                        var extention = Path.GetExtension(f.FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                        {
                            f.CopyTo(fileStreams);
                        }

                        sp.Image += fileName + extention + ";";
                    }
                }

                db.ProductDetails.Add(sp);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(sp);
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductDetail sp, IFormFile file)
        {
            
       
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _environment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"image\product");
                        var extention = Path.GetExtension(file.FileName);
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        sp.Image = fileName + extention;

                    }

                    db.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            
            return View(sp);
        }

        /* public IActionResult Details(int id)
         {
             var items = db.ProductDetails.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size)
                 .FirstOrDefaultAsync(m => m.ProductDetailId == id);
             return View(items);
         }*/
        public IActionResult  Details(int id)
        {
            var items = db.ProductDetails.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size).FirstOrDefault(m => m.ProductDetailId == id);
            return View(items);
        }
        public IActionResult Delete(int id)
        {
            int dem = db.OrderDetails.Where(a => a.ProductDetailId == id).ToList().Count;
            ViewBag.flag = dem;
            Models.ProductDetail x = db.ProductDetails.Find(id);
            return View(x);
        }
        public IActionResult Delete1(int id)
        {
            Models.ProductDetail x = db.ProductDetails.Find(id);
            if (x != null)
            {
                // Lưu trữ đường dẫn của các tệp tin hình ảnh được sử dụng bởi sản phẩm hiện tại
                var usedImagePaths = new HashSet<string>();
                var imageDirectory = new DirectoryInfo(@"wwwroot\image\product2");
                var imageNames = x.Image.Split(";", StringSplitOptions.RemoveEmptyEntries);
                foreach (var imageName in imageNames)
                {
                    var imagePath = Path.Combine(imageDirectory.FullName, imageName);
                    usedImagePaths.Add(imagePath);
                }
                // Xóa các tệp tin không được sử dụng bởi sản phẩm hiện tại
                foreach (var file in imageDirectory.GetFiles())
                {
                    if (!usedImagePaths.Contains(file.FullName))
                    {
                        file.Delete();
                    }
                }
                // Xóa sản phẩm khỏi cơ sở dữ liệu
                db.ProductDetails.Remove(x);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}

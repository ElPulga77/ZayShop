using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;
using PagedList.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZayShop.Controllers
{
    public class ShopController : Controller
    {
        ZayShopContext db = new ZayShopContext();
        public IActionResult Index(string serchproduct, int chonSize = 0)
        {    
            IEnumerable<ProductDetail> items = db.ProductDetails.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            if (!string.IsNullOrEmpty(serchproduct))
            {
                items = items.Where(x => x.Product.ProductName.Contains(serchproduct));
                
            }

            if (chonSize != 0)
            {
                items = db.ProductDetails.AsNoTracking().Where(x => x.SizeId == chonSize);
            }
            ViewBag.DSS = new SelectList(db.Sizes.ToList(), "SizeId", "SizeName");
            return View(items);
        }
        public IActionResult Details(int id)
        {
            
            var items = db.ProductDetails.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size).FirstOrDefault(m => m.ProductDetailId == id);
            ViewBag.DSS = new SelectList(db.Sizes.ToList(), "SizeId", "SizeName");
            return View(items);
        }
    }
}

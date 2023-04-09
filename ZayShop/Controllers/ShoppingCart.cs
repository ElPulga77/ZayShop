using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ZayShop.Controllers
{
    public class ShoppingCart : Controller
    {
        private ZayShopContext db = new ZayShopContext();
        public IActionResult Index()
        {


            return View(db.Products.ToList());
        }
        public IActionResult Cart(int id, int sl)
        {
            Models.OrderProduct hd = MySessions.Get<Models.OrderProduct>(HttpContext.Session, "hoadon");
            if (hd == null)
            {
                hd = new OrderProduct();
            }

            Product product = db.Products.Find(id); // tìm kiếm thông tin chi tiết của sản phẩm theo id truyền vào
          /*  ProductDetail productDetail = db.ProductDetails
            .Include(pd => pd.Size)
            .Include(pd => pd.Color)
            .FirstOrDefault(pd => pd.ProductId == id);

            if (product == null || productDetail == null)
            {
                return NotFound();
            }*/
        /*    OrderDetail orderDetail = hd.OrderDetails.FirstOrDefault(od => od.ProductDetailId == productDetail.ProductDetailId);*/
            OrderDetail b = null;
            foreach (OrderDetail ct in hd.OrderDetails.Where(x => x.ProductDetailId == id))
            {
                b = ct;
                break;
            }
            if (b == null)
            {
                b = new OrderDetail();
                b.ProductDetailId = product.ProductId;
                b.Quantity = sl;
                b.ProductDetail = new ProductDetail();
                b.ProductDetail.Product = product;
                hd.OrderDetails.Add(b);
            }
            else
            {
                b.Quantity += sl;
            }
            MySessions.Set<OrderProduct>(HttpContext.Session, "hoadon", hd);
            return View(hd);
        }


        public IActionResult lapDonhang(OrderProduct x)
        {
            Models.OrderProduct hd = MySessions.Get<Models.OrderProduct>(HttpContext.Session, "hoadon");
            if (hd == null) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                foreach (OrderDetail a in hd.OrderDetails)
                {
                    OrderDetail t = new OrderDetail();
                    t.OrderId = a.OrderId;
                    t.ProductDetailId = a.ProductDetail.ProductId;
                    t.Quantity = a.Quantity;
                    
                    x.OrderDetails.Add(t);
                }

                db.OrderProducts.Add(x);
                db.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        public IActionResult giohang()
        {
            OrderProduct hd = MySessions.Get<OrderProduct>(HttpContext.Session, "hoadon");
            if (hd == null)
            {
                hd = new OrderProduct();
            }
            return View("Cart", hd);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;

namespace ZayShop.Controllers
{
    public class OrderController : Controller
    {
        private ZayShopContext db = new ZayShopContext(); 
        public IActionResult Index()
        {

            var dsSanPham = db.ProductDetails.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            return View(dsSanPham.ToList());
        }
        public IActionResult Cart(int id, int sl)
        {
            Models.OrderProduct hd = MySessions.Get<Models.OrderProduct>(HttpContext.Session, "hoadon");
            if (hd == null)
            {
                hd = new OrderProduct();
            }

            ProductDetail productDetail = db.ProductDetails.Find(id); // tìm kiếm thông tin chi tiết của sản phẩm theo id truyền vào

            Product product = db.Products.Find(productDetail.ProductId); // lấy thông tin sản phẩm tương ứng từ bảng Products
            Size size = db.Sizes.Find(productDetail.SizeId);
            Color color = db.Colors.Find(productDetail.ColorId);
            OrderDetail b = null;
            foreach (OrderDetail ct in hd.OrderDetails.Where(x => x.OrderDetailId == id))
            {
                b = ct;
                break;
            }
            if (b == null)
            {
                b = new OrderDetail();
                b.ProductDetailId = productDetail.ProductDetailId;
                b.Quantity = 1;
                b.ProductDetail = productDetail;
                hd.OrderDetails.Add(b);
            }
            else
            {
                b.Quantity++;
            }
            MySessions.Set<OrderProduct>(HttpContext.Session, "hoadon", hd);
            return View(CChitiethoadonView.getDSChitietHoadon(hd));
        }

      
    
        public IActionResult giohang()
        {
            OrderProduct hd = MySessions.Get<OrderProduct>(HttpContext.Session, "hoadon");
            if (hd == null)
            {
                hd = new OrderProduct();
            }
            return View("Cart", CChitiethoadonView.getDSChitietHoadon(hd));
        }
        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;

namespace ZayShop.Controllers
{
    public class ProductController : Controller
    {
        ZayShopContext db = new ZayShopContext();
        public IActionResult Index()
        {
            var products = db.Products
                        .Include(p => p.ProductDetails)
                            .ThenInclude(pd => pd.Size)
                        .Include(p => p.ProductDetails)
                            .ThenInclude(pd => pd.Color)
                            .Distinct()
                        .ToList();
            return View(products);
            
        }
        public IActionResult Details(int id)
        {

            var products = db.Products
          .Include(p => p.ProductDetails)
              .ThenInclude(pd => pd.Size)
          .Include(p => p.ProductDetails)
              .ThenInclude(pd => pd.Color)
          .AsEnumerable()
          .Distinct()
          .FirstOrDefault(m => m.ProductId == id);
            return View(products);
        }
        public IActionResult Cart(int id)
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
        


    }
}
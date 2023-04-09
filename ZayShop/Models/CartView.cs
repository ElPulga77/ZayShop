using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZayShop.Models
{
    public class CartView
    {
        public string ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public string Gia { get; set; }
        public string Size { get; set; }
        public string color { get; set; }
        public string Hinhanh { get; set; }
        public string Soluong { get; set; }
        public string Thanhtien { get; set; }
        public static List<CartView> getDSChitietHoadons(Models.OrderProduct hd)
        {
            return hd.OrderDetails.Select(x => new CartView
            {
                ProductDetailId = x.ProductDetail.ProductId.ToString(),
                ProductName = x.ProductDetail.Product.ProductName,
                Hinhanh = x.ProductDetail.Product.Image,
                Gia = x.ProductDetail.Product.Price.ToString(),
                Size = x.ProductDetail.Size.SizeName,
                color = x.ProductDetail.Color.ColorName,
                Soluong = x.Quantity.Value.ToString(),
                Thanhtien = (x.Quantity.Value * x.ProductDetail.Product.Price).Value.ToString()
            }).ToList();
        }
    }
}

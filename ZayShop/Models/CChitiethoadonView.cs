using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZayShop.Models
{
    public class CChitiethoadonView
    {
        public string ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public string Gia { get; set; }
        public string Size { get; set; }
        public string color { get; set; }
        public string Hinhanh { get; set; }
        public string Soluong { get; set; }
        public string Thanhtien { get; set; }
        public static List<CChitiethoadonView> getDSChitietHoadon(Models.OrderProduct hd)
        {
            return hd.OrderDetails.Select(x => new CChitiethoadonView
            {
                ProductDetailId = x.ProductDetailId.ToString(),
                ProductName = x.ProductDetail.Product.ProductName,
                Hinhanh = x.ProductDetail.Product.Image,
                Gia = x.ProductDetail.Product.Price.Value.ToString("#,##0"),
               /* Size = x.ProductDetail.Size.SizeName,*/
               /* color = x.ProductDetail.Color.ColorName,*/

                Soluong = x.Quantity.Value.ToString(),
                Thanhtien = (x.Quantity.Value * x.ProductDetail.Product.Price).Value.ToString("#,##0")
            }).ToList();
        }
    }
}

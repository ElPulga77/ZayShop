using System.Collections.Generic;
using System.Linq;

namespace ZayShop.Models
{
    public class Cart
    {
        public int Id { get; set; } // ID của sản phẩm trong giỏ hàng
        public Product Product { get; set; }
        public string Name { get; set; } // Tên sản phẩm
        public decimal Price { get; set; } // Giá sản phẩm
        public int Quantity { get; set; } // Số lượng sản phẩm
        public string ImageUrl { get; set; } // URL hình ảnh sản phẩm
        public string Size { get; set; } // Kích cỡ sản phẩm
        public string Color { get; set; } // Màu sắc sản phẩm
        public decimal TotalPrice { get { return Price * Quantity; } } // Tổng giá tiền của sản phẩm (số lượng * giá)
    }
}

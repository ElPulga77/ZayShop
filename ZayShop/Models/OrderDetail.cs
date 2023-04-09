using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? Quantity { get; set; }
        public virtual OrderProduct Order { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}

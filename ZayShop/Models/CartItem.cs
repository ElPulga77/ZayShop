using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZayShop.Models;
namespace ZayShop.Models
{
    public class CartItem
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public double totalprice { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

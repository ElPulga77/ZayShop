using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class OrderProduct
    {
        public OrderProduct()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? StatusId { get; set; }

        public virtual OrderStatus Status { get; set; }
        public virtual UserAccount User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class Discount
    {
        public int DiscountId { get; set; }
        public string DiscountName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class Product
    {
        public Product()
        {
            Discounts = new HashSet<Discount>();
            Images = new HashSet<Image>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public string Image { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Category Category { get; set; }
        
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}

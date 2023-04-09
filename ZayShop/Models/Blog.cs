using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

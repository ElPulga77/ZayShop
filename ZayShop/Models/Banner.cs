using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Link { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

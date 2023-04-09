using System;
using System.Collections.Generic;

#nullable disable

namespace ZayShop.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

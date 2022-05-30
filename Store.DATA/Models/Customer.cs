using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Store.DATA.Models
{
   public class Customer: IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public string Address { get; set; }

        public int Discount { get; set; } = 0;

        public List<Order> Orders { get; set; } = new List<Order>();

        public Guid? UserId { get; set; } //ключ

        public User User { get; set; } //навигационное свойство
    }
}

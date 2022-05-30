using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Store.DATA.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        [Required]
        public int ItemCount { get; set; }

        [Required]
        public int ItemPrice { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }
    }
}

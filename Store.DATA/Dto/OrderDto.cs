using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Store.DATA.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? ShipmentDate { get; set; } = null;

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public string Status { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public CustomerDto Customer { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}

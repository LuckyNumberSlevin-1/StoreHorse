using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Store.DATA.Dto
{
    public class  ItemDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Category { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Store.DATA.Dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public string Address { get; set; }

        public int Discount { get; set; } = 0;

        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();

        public Guid? UserId { get; set; }

        public UserDto User { get; set; }

        public string StoreUserName { get; set; }

        public string Password { get; set; }
    }
}

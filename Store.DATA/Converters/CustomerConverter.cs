using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DATA.Dto;
using Store.DATA.Models;

namespace Store.DATA.Converters
{
    public static class CustomerConverter
    {
        public static Customer Convert(CustomerDto item) =>
            new Customer
            {
                Address = item.Address,
                Code = item.Code,
                UserName = item.UserName,
                Id = item.Id,
                Discount = item.Discount,
                Name = item.Name,
                UserId = item.UserId
            };

        public static CustomerDto Convert(Customer item) =>
            new CustomerDto
            {
                Address = item.Address,
                Code = item.Code,
                UserName = item.UserName,
                Id = item.Id,
                Discount = item.Discount,
                Name = item.Name,
                UserId = item.UserId
            };

        public static List<Customer> Convert(List<CustomerDto> items) =>
            items.Select(c => Convert(c)).ToList();
        public static List<CustomerDto> Convert(List<Customer> items) =>
            items.Select(c => Convert(c)).ToList();
    }
}

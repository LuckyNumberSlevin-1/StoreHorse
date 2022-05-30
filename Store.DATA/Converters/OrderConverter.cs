using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DATA.Dto;
using Store.DATA.Models;

namespace Store.DATA.Converters
{
    public static class OrderConverter
    {
        public static Order Convert(OrderDto item)
        {
           var order = new Order
            {
                Id = item.Id,
                OrderDate = item.OrderDate,
                ShipmentDate = item.ShipmentDate,
                OrderNumber = item.OrderNumber,
                Status = item.Status,
                CustomerId = item.CustomerId,
            };
            if (item.OrderItems != null)
                order.OrderItems = OrderItemConverter.Convert(item.OrderItems);
            return order;
        }

        public static OrderDto Convert(Order item) =>
            new OrderDto
            {
                Id = item.Id,
                OrderDate = item.OrderDate,
                ShipmentDate = item.ShipmentDate,
                OrderNumber = item.OrderNumber,
                Status = item.Status,
                CustomerId = item.CustomerId,
            };

        public static List<Order> Convert(List<OrderDto> items) =>
           items.Select(c => Convert(c)).ToList();

        public static List<OrderDto> Convert(List<Order> items) =>
            items.Select(c => Convert(c)).ToList();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DATA.Dto;
using Store.DATA.Models;

namespace Store.DATA.Converters
{
    public static class OrderItemConverter
    {
        public static OrderItem Convert(OrderItemDto item) =>
            new OrderItem
            {
                Id=item.Id,
                ItemCount=item.ItemCount,
                ItemPrice=item.ItemPrice,
                OrderId=item.OrderId,
                ItemId=item.ItemId,
            };
        public static OrderItemDto Convert(OrderItem item) =>
            new OrderItemDto
            {
                Id = item.Id,
                ItemCount = item.ItemCount,
                ItemPrice = item.ItemPrice,
                OrderId = item.OrderId,
                ItemId = item.ItemId,
            };

        public static List<OrderItem> Convert(List<OrderItemDto> items) =>
            items.Select(c => Convert(c)).ToList();

        public static List<OrderItemDto> Convert(List<OrderItem> items) =>
            items.Select(c => Convert(c)).ToList();
    }
}

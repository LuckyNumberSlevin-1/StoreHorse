using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DATA.Dto;
using Store.DATA.Models;

namespace Store.DATA.Converters
{
    public static class ItemConverter
    {
        public static Item Convert(ItemDto item) =>
            new Item
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category
            };

        public static ItemDto Convert(Item item) =>
            new ItemDto
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category
            };

        public static List<Item> Convert(List<ItemDto> items) =>
            items.Select(c => Convert(c)).ToList();
        public static List<ItemDto> Convert(List<Item> items) =>
           items.Select(c => Convert(c)).ToList();
    }
}

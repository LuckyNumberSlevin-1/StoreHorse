using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.CORE.EF_dbContext;
using Store.DATA.Repositories;
using Store.DATA.Models;
using Store.DATA.Dto;
using Store.DATA.Converters;

namespace Store.CORE.Repo_App
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationContext _context;
        public OrderItemRepository(ApplicationContext context) => _context = context;
        
        public async Task<OrderItemDto> CreateAsync(OrderItemDto item)
        {
            try
            {
                var itm = await _context.Items.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == item.ItemId);
                if (itm == null)
                    return null;
                item.ItemPrice = itm.Price;
                var result = await _context.OrderItems.AddAsync(OrderItemConverter.Convert(item));
                await _context.SaveChangesAsync();
                return OrderItemConverter.Convert(result.Entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var orderItem = await _context.OrderItems.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id ==id);
                if (orderItem == null)
                    return false;
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<OrderItemDto>> GetAllAsync()
        {
            try
            {
                return await _context.OrderItems.AsNoTracking().Select(x => new OrderItemDto
                {
                    Id = x.Id,
                    ItemCount = x.ItemCount,
                    ItemId = x.ItemId,
                    ItemName = x.Item.Name,
                    ItemPrice = x.ItemPrice,
                    OrderId = x.OrderId,
                    OrderNumber = x.Order.OrderNumber
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OrderItemDto> GetByIdAsync(Guid id)
        {
            return await _context.OrderItems.AsNoTracking().Select(x => new OrderItemDto
            {
                Id = x.Id,
                ItemCount = x.ItemCount,
                ItemId = x.ItemId,
                ItemName = x.Item.Name,
                ItemPrice = x.ItemPrice,
                OrderId = x.OrderId,
                OrderNumber = x.Order.OrderNumber,
                Item = ItemConverter.Convert(x.Item),
                Order = OrderConverter.Convert(x.Order)
            }).FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task<List<OrderItemDto>> GetByOrderIdAsync(Guid id)
        {
            try
            {
                return await _context.OrderItems.AsNoTracking().Select(x => new OrderItemDto
                {
                    Id = x.Id,
                    ItemCount = x.ItemCount,
                    ItemId = x.ItemId,
                    ItemName = x.Item.Name,
                    ItemPrice = x.ItemPrice,
                    OrderId = x.OrderId,
                    OrderNumber = x.Order.OrderNumber,
                    Item = ItemConverter.Convert(x.Item),
                    Order = OrderConverter.Convert(x.Order)
                }).Where(y => y.OrderId == id).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(OrderItemDto item)
        {
            try
            {
                var orderItem = await _context.OrderItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (orderItem == null)
                    return false;
                _context.OrderItems.Update(OrderItemConverter.Convert(item));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

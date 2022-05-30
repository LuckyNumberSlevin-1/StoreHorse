using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Store.DATA.Models;
using Store.DATA.Dto;
using Store.DATA.Repositories;
using Store.CORE.EF_dbContext;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Store.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="customer")]
    public class AccountController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationContext _context;

        public AccountController(IOrderRepository repo, ApplicationContext context)
        {
            _orderRepository = repo;
            _context = context;
        }

        [HttpGet]
        [Produces(typeof(List<OrderDto>))]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            try
            {
                var id = await GetUserId();
                if (id != Guid.Empty)
                    return Ok(await _orderRepository.GetByCustomerIdAsync(id));
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> DeleteOrder(Guid id)
        {
            try
            {
                var i = await GetUserId();
                if (i != Guid.Empty)
                {
                    var order = await _orderRepository.GetByIdAsync(id);
                    if (order.CustomerId == i && order.Status == "Новый")
                        return Ok(await _orderRepository.DeleteAsync(id));
                }
                return false;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> AddOrder([FromBody] OrderDto item)
        {
            try
            {
                var id = await GetUserId();
                if(id!=Guid.Empty)
                    {
                        item.CustomerId = id;
                        return Ok(await _orderRepository.CreateAsync(item));
                    }
                return false;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        private async Task<Guid> GetUserId()
        {
            try
            {
                var i = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var res = await _context.Customers.AsNoTracking().Select(x => new
                {
                    login = x.UserName,
                    id = x.Id
                }).FirstOrDefaultAsync(y => y.login == i);
                return res.id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}

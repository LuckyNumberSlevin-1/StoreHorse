using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Store.DATA.Models;
using Store.DATA.Dto;
using Store.DATA.Repositories;

namespace Store.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository order) => _orderRepository = order;

        [HttpGet]
        [Produces(typeof(List<OrderDto>))]
        [Authorize(Roles ="manager")]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            try
            {
                return Ok(await _orderRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(OrderDto))]
        [Authorize(Roles = "manager")]

        public async Task <ActionResult<OrderDto>> Get(Guid id)
        {
            try
            {
                var result = await _orderRepository.GetByIdAsync(id);
                if (result == null)
                    return BadRequest();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("number/{number}")]
        [Produces(typeof(OrderDto))]
        public async Task<ActionResult<OrderDto>> Get(int num)
        {
            try
            {
                var result = await _orderRepository.GetByOrderNumberAsync(num);
                if (result == null)
                    return BadRequest();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }   
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="manager")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _orderRepository.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(OrderDto))]
        [Authorize(Roles ="manager")]
        public async Task<ActionResult<OrderDto>> Post([FromBody] OrderDto dto)
        {
            try
            {
                var result = await _orderRepository.CreateAsync(dto);
                if (result == null)
                    return BadRequest();

                return result;

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut]
        [Produces(typeof(bool))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<bool>> Put([FromBody] OrderDto dto)
        {
            try
            {
                return Ok(await _orderRepository.UpdateAsync(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
       
    }
}

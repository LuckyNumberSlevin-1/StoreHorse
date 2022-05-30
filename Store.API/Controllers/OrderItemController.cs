using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Store.DATA.Dto;
using Store.DATA.Repositories;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository repository) => _orderItemRepository = repository;

        [HttpGet]
        [Produces(typeof(List<OrderItemDto>))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderItemDto>>> Get()
        {
            try
            {
                return Ok(await _orderItemRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(OrderItemDto))]
        public async Task<ActionResult<OrderItemDto>> Get(Guid id)
        {
            try
            {
                var result = await _orderItemRepository.GetByIdAsync(id);
                if (result == null)
                    return BadRequest();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("order/{id}")]
        [Produces(typeof(List<OrderItemDto>))]
        public async Task<ActionResult<List<OrderItemDto>>> GetByOrderId(Guid id)
        {
            try
            {
                var result = await _orderItemRepository.GetByOrderIdAsync(id);
                if (result == null)
                    return null;

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _orderItemRepository.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(OrderItemDto))]
        public async Task<ActionResult<OrderItemDto>> Post([FromBody] OrderItemDto itemDto)
        {
            try
            {
                var result = await _orderItemRepository.CreateAsync(itemDto);
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
        public async Task<ActionResult<bool>> Put([FromBody] OrderItemDto itemDto)
        {
            try
            {
                return Ok(await _orderItemRepository.UpdateAsync(itemDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
       
    }
}

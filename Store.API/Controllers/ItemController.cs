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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository repository) => _itemRepository = repository;

        [HttpGet]
        [Produces(typeof(List<ItemDto>))]
        public async Task<ActionResult<List<ItemDto>>> Get()
        {
            try
            {
                return Ok(await _itemRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(ItemDto))]
        public async Task<ActionResult<ItemDto>> Get(Guid id)
        {
            try
            {
                var result = await _itemRepository.GetByIdAsync(id);
                if (result == null)
                    return BadRequest();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("code/{code}")]
        [Produces(typeof(ItemDto))]
        public async Task<ActionResult<ItemDto>> Get(string code)
        {
            try
            {
                var result = await _itemRepository.GetByCodeAsync(code);
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
        [Produces(typeof(bool))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _itemRepository.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(ItemDto))]
        [Authorize(Roles ="manager")]
        public async Task<ActionResult<ItemDto>> Post([FromBody] ItemDto dto)
        {
            try
            {
                var result = await _itemRepository.CreateAsync(dto);
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
        [Authorize(Roles ="manager")]
        public async Task<ActionResult<bool>> Put([FromBody] ItemDto dto)
        {
            try
            {
                return Ok(await _itemRepository.UpdateAsync(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}

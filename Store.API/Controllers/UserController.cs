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
    [Authorize(Roles = "manager")] //Весь контроллер доступен только админу
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository user) => _userRepository = user;

        [HttpGet]
        [Produces(typeof(List<UserDto>))]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            try
            {
                return Ok(await _userRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        [Produces(typeof(UserDto))]
        public async Task<ActionResult<UserDto>> Get(Guid id)
        {
            try
            {
                var result = await _userRepository.GetByIdAsync(id);
                if (result == null)
                    return result;

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
                return Ok(await _userRepository.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(UserDto))]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto user)
        {
            try
            {
                var result = await _userRepository.CreateAsync(user);
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
        public async Task<ActionResult<bool>> Put([FromBody] UserDto user)
        {
            try
            {
                return Ok(await _userRepository.UpdateAsync(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

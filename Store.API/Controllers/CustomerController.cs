using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.API.ViewModels;
using Store.AUTHETICATION.Interfaces;
using Store.DATA.Dto;
using Microsoft.AspNetCore.Authorization;
using Store.DATA.Repositories;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customer) => _customerRepository = customer;

        [HttpGet("id")]
        [Produces(typeof(List<CustomerDto>))]
        public async Task<ActionResult<List<CustomerDto>>> Get()
        {
            try
            {
                return Ok(await _customerRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("code/{code}")]
        [Produces(typeof(CustomerDto))]
        public async Task<ActionResult<CustomerDto>> Get(string code)
        {
            try
            {
                var result = await _customerRepository.GetByCodeAsync(code);
                if (result == null)
                    return BadRequest();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("user/{id}")]
        [Produces(typeof(List<CustomerDto>))]
        public async Task<ActionResult<List<CustomerDto>>> GetByUserId(Guid id)
        {
            try
            {
                var result = await _customerRepository.GetByUserIdAsync(id);
                if (result == null) 
                    return BadRequest();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("id")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _customerRepository.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


    }
}

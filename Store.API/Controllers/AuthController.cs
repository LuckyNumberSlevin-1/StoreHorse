using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.API.ViewModels;
using Store.AUTHETICATION.Interfaces;
using Store.DATA.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Store.API.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service) => _authService = service;


        [HttpPost]
        [AllowAnonymous]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Login([FromBody] LoginViewModel viewModel)
        {
            try
            {
                var result = await _authService.Login(viewModel.Login, viewModel.Password);
                if (result == null)
                    return BadRequest();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody] CustomerDto dto)
        {
            try
            {
                var result = await _authService.Register(dto);
                if (result == null)
                    BadRequest();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex); 
            }
        }

    }
}

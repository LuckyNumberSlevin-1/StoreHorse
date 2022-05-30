using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.AUTHETICATION.Interfaces;
using Store.DATA.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.DATA.Dto;
using Store.CORE.EF_dbContext;
using Microsoft.EntityFrameworkCore;
using Store.CORE.Repo_App;
using Store.DATA.Converters;

namespace Store.AUTHETICATION.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ApplicationContext _context;
        private readonly IJwtService _jwt;

        public AuthService(UserManager<Customer> uM, SignInManager<Customer> sim,
             ApplicationContext context, IJwtService jwt)
        {
            _userManager = uM;
            _signInManager = sim;
            _context = context;
            _jwt = jwt;
        }

        public async Task<object> Login(string log, string pass)
        {
            if (log == null || pass == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(log, pass, false, false);

            if(result.Succeeded)
            {
                var userApp = await _userManager.FindByNameAsync(log);
                return await _jwt.GenerateJwt(userApp);
            }
            return null;
        }

        public async Task<object> Register(CustomerDto dto)
        {
            int count = _context.Customers.Count();
            dto.Code = $"{count.ToString("0000")}-{DateTime.Now.Year}";

            while ((await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x=>x.Code==dto.Code))!=null)
            {
                ++count;
                dto.Code = $"{count.ToString("0000")}-{DateTime.Now.Year}";
            }

            var user = CustomerConverter.Convert(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "customer");
                return await _jwt.GenerateJwt(user);
            }

            return null;
        }
    }
}

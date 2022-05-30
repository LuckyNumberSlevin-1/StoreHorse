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

namespace Store.AUTHETICATION.Services
{
    public class JwtGenerator : IJwtService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;

        public JwtGenerator(UserManager<Customer> manager, IConfiguration configuration)
        {
            _userManager = manager;
            _configuration = configuration;
        }

        public async Task<object> GenerateJwt(Customer user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key"]));
            var siCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));

            var token = new JwtSecurityToken(
                _configuration["Issuer"],
                _configuration["Audience"],
                claimsIdentity.Claims,
                expires: expires,
                signingCredentials: siCred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

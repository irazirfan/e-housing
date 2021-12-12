using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos;
using webApi.Interfaces;
using webApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace webApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;
        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.uow = uow;
            this.configuration = configuration;   
        }


        // api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.Username, loginReq.Password);

            if(user == null)
            {
                return Unauthorized();
            }
            
            var loginRes = new LoginResDto();
            loginRes.Username = user.Username;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }

        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));
            
            var claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key,SecurityAlgorithms.HmacSha256Signature);
            
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
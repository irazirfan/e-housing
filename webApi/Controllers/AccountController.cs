using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos;
using webApi.Interfaces;

namespace webApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        public AccountController(IUnitOfWork uow)
        {
            this.uow = uow;
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
            loginRes.Token = "Token to be generated";
            return Ok(loginRes);
        }
    }
}
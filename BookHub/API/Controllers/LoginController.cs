using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResult>> Login(LoginUserDTO loginUserDTO)
        {
            var loginResult = await _loginService.LoginAsync(loginUserDTO.Username, loginUserDTO.Password);
            return Ok(loginResult);
        }
    }
}

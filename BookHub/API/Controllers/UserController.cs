using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;

        public UserController(IUserService userService, IPhotoService photoService)
        {
            _userService = userService;
            _photoService = photoService;
        }

        [HttpGet("Id/{id}")]
        public async Task<ActionResult<BookUserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserInfoByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("Username/{username}")]
        public async Task<ActionResult<BookUserDTO>> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserInfoByUsernameAsync(username);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> SaveProfileChanges(BookUserDTO bookUser)
        {
            bool updated = await _userService.SaveProfileChangesAsync(bookUser);
            return Ok(updated);
        }

        [HttpPost("UploadPhoto")]
        public async Task<ActionResult<bool>> SaveFile(IFormFile file)
        {
            bool uploaded = await _photoService.SavePhoto("ProfilePhotos", file);
            return Ok(uploaded);
        }
    }
}

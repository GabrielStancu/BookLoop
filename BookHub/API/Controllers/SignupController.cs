using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController: ControllerBase
    {
        private readonly ISignupService _signupCheckService;
        private readonly IPhotoService _photoService;

        public SignupController(ISignupService signupCheckService, IPhotoService photoService)
        {
            _signupCheckService = signupCheckService;
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<ActionResult<SignupResult>> SignUp(SignupUserDTO signupUserDTO)
        {
            var signupResult = await _signupCheckService.SignupAsync(signupUserDTO);
            return Ok(signupResult);
        }

        [HttpPost("UploadPhoto")]
        public async Task<ActionResult<bool>> SaveFile(IFormFile file)
        {
            bool uploaded = await _photoService.SavePhoto("ProfilePhotos", file);
            return Ok(uploaded);
        }
    }
}

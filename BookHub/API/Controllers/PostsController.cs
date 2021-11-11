using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly IPhotoService _photoService;

        public PostsController(
            IPostsService postsService,
            IPhotoService photoService)
        {
            _postsService = postsService;
            _photoService = photoService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> MakePost(BookPost bookPost)
        {
            var posted = await _postsService.MakePostAsync(bookPost);
            return Ok(posted);
        }

        [HttpPut]
        public async Task<ActionResult<BooksSearchResultDTO>> GetPostsWithSpecification(Specification specification)
        {
            var searchResult = await _postsService.GetPostsWithSpecAsync(specification);
            return Ok(searchResult);
        }

        [HttpPost("UploadPhoto")]
        public async Task<ActionResult<bool>> SaveFile(IFormFile file)
        {
            bool uploaded = await _photoService.SavePhoto("BookPosts", file);
            return Ok(uploaded);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePost(int id)
        {
            bool deleted = await _postsService.DeletePostAsync(id);
            return Ok(deleted);
        }
    }
}

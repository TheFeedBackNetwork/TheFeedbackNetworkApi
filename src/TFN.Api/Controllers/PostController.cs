using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Models.InputModels;
using TFN.Api.Models.ModelBinders;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : AppController
    {
        public IPostRepository PostRepository { get; private set; }
        public PostController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        [HttpGet]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAllAsync(
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short offset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short limit = 100)
        {

            throw new NotImplementedException();
        }

        [HttpGet("{postId:Guid}")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}")]
        [Authorize("posts.write")]
        public async Task<IActionResult> GetAsync(Guid postId, Guid commentId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync([FromBody]PostInputModel post)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{postId:Guid}/comments")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync([FromBody]CommentInputModel comment)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{postId:Guid}")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchAsync([FromBody]PostInputModel post)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{postId:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid postId, Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}

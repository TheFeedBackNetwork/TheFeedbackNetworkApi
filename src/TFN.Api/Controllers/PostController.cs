using System;
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

        [HttpGet(Name = "GetAllPosts")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAllAsync(
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short postOffset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short postlimit = 100,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short commentOffset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short commentLimit = 100)
        {

            throw new NotImplementedException();
        }

        [HttpGet("{postId:Guid}", Name = "GetPost")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}", Name = "GetComment")]
        [Authorize("posts.write")]
        public async Task<IActionResult> GetAsync(Guid postId, Guid commentId)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = "PostPost")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync([FromBody]PostInputModel post)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{postId:Guid}/comments", Name = "PostComment")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync([FromBody]CommentInputModel comment)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{postId:Guid}", Name = "EditPost")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchAsync([FromBody]PostInputModel post)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{postId:Guid}", Name = "DeletePost")]
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

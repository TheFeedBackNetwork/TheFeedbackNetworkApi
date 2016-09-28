using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Api.Models.InputModels;
using TFN.Api.Models.ModelBinders;
using TFN.Api.Models.QueryModels;
using TFN.Api.Models.ResponseModels;
using TFN.Api.Extensions;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;

namespace TFN.Api.Controllers
{
    
    [Route("api/posts")]
    public class PostController : AppController
    {
        public IPostRepository PostRepository { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        public PostController(IPostRepository postRepository, IAuthorizationService authorizationService)
        {
            PostRepository = postRepository;
            AuthorizationService = authorizationService;
        }

        [HttpGet(Name = "GetAllPosts")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery]ExcludeQueryModel exclude,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short postOffset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short postlimit = 7,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short commentOffset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short commentLimit = 25)
        {
            var posts = await PostRepository.GetAllAsync(postOffset, postlimit, commentOffset, commentLimit);
            var model = posts.Select(x => PostResponseModel.From(x, AbsoluteUri));
            if (exclude != null)
            {
                return this.Json(model,exclude.Attributes);
            }
            return Json(model);
        }

        [HttpGet("{postId:Guid}", Name = "GetPost")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(
            Guid postId,
            [FromQuery]ExcludeQueryModel exclude,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short commentOffset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short commentLimit = 25)
        {
            var post = await PostRepository.GetAsync(postId, commentOffset, commentLimit);

            if (post == null)
            {
                return NotFound();
            }

            var model = PostResponseModel.From(post, AbsoluteUri);

            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }
            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}", Name = "GetComment")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(
            Guid postId,
            Guid commentId,
            [FromQuery]ExcludeQueryModel exclude)
        {
            var comment = await PostRepository.GetAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var model = CommentResponseModel.From(comment, AbsoluteUri);

            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}/scores/{scoreId:Guid}", Name = "GetScore")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            var score = await PostRepository.GetAsync(postId, commentId, scoreId);

            if (score == null)
            {
                return NotFound();
            }

            var model = ScoreResponseModel.From(score, AbsoluteUri);

            return Json(model);
        }

        [HttpPost(Name = "PostPost")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync([FromBody]PostInputModel post)
        {
            var genre = Genre.Other;
            var parsed = Enum.TryParse(post.Genre.ToString(), out genre);

            if (!parsed)
            {
                return BadRequest();
            }
            var entity = new Post(UserId, Username, post.TrackUrl, post.Text,genre,post.Tags);

            var authZModel = PostAuthorizationModel.From(entity);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Write))
            {
                return new ChallengeResult();
            }

            await PostRepository.AddAsync(entity);

            var model = PostResponseModel.From(entity, AbsoluteUri);

            return CreatedAtAction("GetPost", new {postId = model.Id}, model);
        }

        [HttpPost("{postId:Guid}/comments", Name = "PostComment")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync(
            Guid postId,
            [FromBody]CommentInputModel comment)
        {
            var post = await PostRepository.GetAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var entity = new Comment(UserId,postId,Username,comment.Text);

            var authZModel = CommentAuthorizationModel.From(entity);

            if (await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Write))
            {
                return new ChallengeResult();
            }

            await PostRepository.AddAsync(entity);

            var model = CommentResponseModel.From(entity, AbsoluteUri);

            return CreatedAtAction("GetComment", new {postId = model.PostId, commentId = model.Id}, model);
        }

        [HttpPost("{postId:Guid}/comments/{commentId:Guid}/scores", Name = "PostScore")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync(
            Guid postId,
            Guid commentId)
        {
            var comment = await PostRepository.GetAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var entity = new Score(commentId, UserId, Username);


            var authZModel = ScoreAuthorizationModel.From(entity, comment.UserId);

            if (await AuthorizationService.AuthorizeAsync(User, authZModel, ScoreOperations.Write))
            {
                return new ChallengeResult();
            }

            await PostRepository.AddAsync(entity);

            var model = ScoreResponseModel.From(entity, AbsoluteUri);

            return CreatedAtAction("GetScore", new { postId = comment.PostId, commentId = model.CommentId, scoreId = model.Id }, model);
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        [HttpPatch("{postId:Guid}", Name = "EditPost")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchAsync(
            Guid postId,
            [FromBody]PostInputModel post)
        {
            throw new NotImplementedException();
        }
        #pragma warning disable 1998
        //TODO Remove when we async
        [HttpPatch("{postId:Guid}/comments/{commentId:Guid}", Name = "EditComment")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchAsync(
            Guid postId,
            Guid commentId,
            [FromBody]CommentInputModel post)
        {
            throw new NotImplementedException();
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        [HttpDelete("{postId:Guid}", Name = "DeletePost")]
        public async Task<IActionResult> DeleteAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteAsync(Guid postId, Guid commentId)
        {
            throw new NotImplementedException();
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}/scores/{scoreId:Guid}", Name = "DeleteScore")]
        public async Task<IActionResult> DeleteAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            throw new NotImplementedException();
        }
    }
}

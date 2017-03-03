using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Api.Models.InputModels;
using TFN.Api.Models.ModelBinders;
using TFN.Api.Models.QueryModels;
using TFN.Api.Models.ResponseModels;
using TFN.Api.Extensions;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using TFN.Domain.Models.ValueObjects;
using TFN.Mvc.HttpResults;

namespace TFN.Api.Controllers
{
    
    [Route("api/posts")]
    public class PostController : AppController
    {
        public IPostService PostService { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        public ICreditService CreditService { get; private set; }
        public PostController(IPostService postService, IAuthorizationService authorizationService, ICreditService creditService)
        {
            PostService = postService;
            AuthorizationService = authorizationService;
            CreditService = creditService;
        }

        [HttpGet(Name = "GetAllPosts")]
        //[Authorize("posts.read")]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery]ExcludeQueryModel exclude,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]int offset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var c = Thread.CurrentThread.CurrentCulture;

            var posts = await PostService.GetAllPostsAsync(offset, limit);

            var summaries = new List<PostSummary>();

            foreach (var post in posts)
            {
                var summary =  await PostService.GetPostLikeSummaryAsync(post.Id, 5, Username);
                summaries.Add(summary);
            }

            var model = new List<PostResponseModel>();
            foreach (var post in posts)
            {
                var summary = summaries.SingleOrDefault(x => x.PostId == post.Id);
                var credits = await CreditService.GetByUserIdAsync(post.UserId);
                var authZ = ResourceAuthorizationResponseModel.From(post, HttpContext, Caller);
                model.Add(PostResponseModel.From(post,summary,credits,authZ,AbsoluteUri));
            }

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
            [FromQuery]ExcludeQueryModel exclude)
        {
            var post = await PostService.GetPostAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var summary = await PostService.GetPostLikeSummaryAsync(postId, 5, Username);
            var credits = await CreditService.GetByUserIdAsync(post.UserId);
            var authZ = ResourceAuthorizationResponseModel.From(post, HttpContext, Caller);
            var model = PostResponseModel.From(post, summary, credits,authZ, AbsoluteUri);


            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }
            return Json(model);
        }

        [HttpGet("{postId:Guid}/likes", Name = "GetPostSummary")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetPostLikesSummaryAsync(
            Guid postId,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var post = await PostService.GetPostAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            var postSummary = await PostService.GetPostLikeSummaryAsync(postId, limit, Username);
            if (postSummary == null)
            {
                return NotFound();
            }

            var credits = await CreditService.GetByUserIdAsync(post.UserId);
            var model = PostSummaryResponseModel.From(postSummary, credits, AbsoluteUri);

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}", Name = "GetComment")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(
            Guid postId,
            Guid commentId,
            [FromQuery]ExcludeQueryModel exclude)
        {
            var comment = await PostService.GetCommentAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var summary = await PostService.GetCommentScoreSummaryAsync(commentId, 5, Username);
            var credits = await CreditService.GetByUserIdAsync(comment.UserId);
            var authZ = ResourceAuthorizationResponseModel.From(comment, HttpContext, Caller);
            var model = CommentResponseModel.From(comment,summary,credits,authZ,AbsoluteUri);


            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments", Name = "GetComments")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetCommentsAsync(
            Guid postId,
            [FromQuery]ExcludeQueryModel exclude,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]int offset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var comments = await PostService.GetCommentsAsync(postId, offset, limit);

            if (comments == null)
            {
                return NotFound();
            }

            var summaries = new List<CommentSummary>();
            foreach (var comment in comments)
            {
                var summary = await PostService.GetCommentScoreSummaryAsync(comment.Id, 5, Username);
                summaries.Add(summary);
            }

            var model = new List<CommentResponseModel>();
            foreach (var comment in comments)
            {
                var summary = summaries.SingleOrDefault(x => x.CommentId == comment.Id);
                var credits = await CreditService.GetByUserIdAsync(comment.UserId);
                var authZ = ResourceAuthorizationResponseModel.From(comment, HttpContext, Caller);
                model.Add(CommentResponseModel.From(comment,summary,credits,authZ,AbsoluteUri));
            }


            if (exclude != null)
            {
                return this.Json(model, exclude.Attributes);
            }

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}/scores", Name = "GetCommentSummary")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetCommentScoreSummaryAsync(
            Guid postId,
            Guid commentId,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var comment = await PostService.GetCommentAsync(postId,commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var commentSummary = await PostService.GetCommentScoreSummaryAsync(commentId, limit, Username);
            if (commentSummary == null)
            {
                return NotFound();
            }
            var credits = await CreditService.GetByUserIdAsync(comment.UserId);
            var model = CommentSummaryResponseModel.From(commentSummary,credits, AbsoluteUri, postId);

            return Json(model);
        }

        [HttpGet("{postId:Guid}/comments/{commentId:Guid}/scores/{scoreId:Guid}", Name = "GetScore")]
        [Authorize("posts.read")]
        public async Task<IActionResult> GetAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            var score = await PostService.GetScoreAsync(commentId, scoreId);

            if (score == null)
            {
                return NotFound();
            }

            var model = ScoreResponseModel.From(score, AbsoluteUri, postId);

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

            var credits = await CreditService.GetByUserIdAsync(UserId);
            if (credits == null)
            {
                //something really wrong happened
                return new HttpBadRequestResult("No credits resource for the account please contact support");
            }

            var creditAuthZModel = CreditsAuthorizationModel.From(credits);

            if (!await AuthorizationService.AuthorizeAsync(User, creditAuthZModel, CreditsOperations.Delete))
            {
                return new HttpForbiddenResult("An attempt to use up credits was attempted, but the authorization policy challenged the request");
            }

            var authZModel = PostAuthorizationModel.From(entity);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Write))
            {
                return new HttpForbiddenResult("A POST request for adding a new post resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.AddAsync(entity);
            var summary = await PostService.GetPostLikeSummaryAsync(entity.Id, 5, Username);
            await CreditService.ReduceCreditsAsync(credits, 5);
            credits = await CreditService.GetByUserIdAsync(entity.UserId);
            var authZ = ResourceAuthorizationResponseModel.From(entity, HttpContext, Caller);
            var model = PostResponseModel.From(entity,summary,credits,authZ, AbsoluteUri);

            

            return CreatedAtAction("GetPost", new {postId = model.Id}, model);
        }

        [HttpPost("{postId:Guid}/comments", Name = "PostComment")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync(
            Guid postId,
            [FromBody]CommentInputModel comment)
        {
            var post = await PostService.GetPostAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var entity = new Comment(UserId,postId,Username,comment.Text);

            var authZModel = CommentAuthorizationModel.From(entity);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Write))
            {
                return new HttpForbiddenResult("A POST request for adding a new post comment resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.AddAsync(entity);

            var summary = await PostService.GetCommentScoreSummaryAsync(entity.Id, 5, Username);

            var credits = await CreditService.GetByUserIdAsync(entity.UserId);
            var authZ = ResourceAuthorizationResponseModel.From(entity, HttpContext, Caller);
            var model = CommentResponseModel.From(entity, summary,credits, authZ ,AbsoluteUri);

            return CreatedAtAction("GetComment", new {postId = model.PostId, commentId = model.Id}, model);
        }

        [HttpPost("{postId:Guid}/comments/{commentId:Guid}/scores", Name = "PostScore")]
        [Authorize("posts.write")]
        public async Task<IActionResult> PostAsync(
            Guid postId,
            Guid commentId,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]int offset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]int limit = 7)
        {
            var comment = await PostService.GetCommentAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var scores = await PostService.GetAllScoresAsync(commentId, offset, limit);

            if(scores.Any(x => x.UserId == UserId))
            {
                return BadRequest();
            }

            var entity = new Score(commentId, UserId, Username);

            var authZModel = ScoreAuthorizationModel.From(entity, comment.UserId);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, ScoreOperations.Write))
            {
                return new HttpForbiddenResult("A POST request for adding a new comment score resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.AddAsync(entity);
            await CreditService.AwardCreditAsync(UserId, comment.UserId, 1);

            var model = ScoreResponseModel.From(entity, AbsoluteUri, postId);


            return CreatedAtAction("GetScore", new { postId = comment.PostId, commentId = model.CommentId, scoreId = model.Id }, model);
        }

        [HttpPatch("{postId:Guid}", Name = "EditPost")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchAsync(
            Guid postId,
            [FromBody]PostInputModel model)
        {
            var post = await PostService.GetPostAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            if (NodaTime.SystemClock.Instance.Now.Minus(post.Created) > Duration.FromHours(1))
            {
                return new HttpBadRequestResult("Post Cannot be edited 1 hour after its creation.");
            }

            var genre = Genre.Other;
            var parsed = Enum.TryParse(post.Genre.ToString(), out genre);

            var authZModel = PostAuthorizationModel.From(post);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Edit))
            {
                return new HttpForbiddenResult("A PATCH request for ammending a post resource was attempted, but the authorization policy challenged the request.");
            }

            var editedPost = Post.EditPost(post, model.Text, model.TrackUrl, model.Tags, genre);

            await PostService.UpdateAsync(editedPost);

            return NoContent();
        }


        [HttpPatch("{postId:Guid}/comments/{commentId:Guid}", Name = "EditComment")]
        [Authorize("posts.edit")]
        public async Task<IActionResult> PatchAsync(
            Guid postId,
            Guid commentId,
            [FromBody]CommentInputModel post)
        {
            var comment = await PostService.GetCommentAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var authZModel = CommentAuthorizationModel.From(comment);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Edit))
            {
                return new HttpForbiddenResult("A PATCH request for ammending a comment resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.UpdateAsync(comment);

            return NoContent();
        }


        [HttpDelete("{postId:Guid}", Name = "DeletePost")]
        public async Task<IActionResult> DeleteAsync(Guid postId)
        {
            var post = await PostService.GetPostAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var authZModel = PostAuthorizationModel.From(post);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Delete))
            {
                return new HttpForbiddenResult("A DELETE request for deleting a post resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.DeletePostAsync(postId);

            return Ok();
        }

        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteAsync(Guid postId, Guid commentId)
        {
            var comment = await PostService.GetCommentAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var authZModel = CommentAuthorizationModel.From(comment);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CommentOperations.Delete))
            {
                return new HttpForbiddenResult("A DELETE request for deleting a post's comment resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.DeleteCommentAsync(commentId);

            return Ok();
        }

        [HttpDelete("{postId:Guid}/comments/{commentId:Guid}/scores/{scoreId:Guid}", Name = "DeleteScore")]
        public async Task<IActionResult> DeleteAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            var score = await PostService.GetScoreAsync(commentId,scoreId);

            if (score == null)
            {
                return NotFound();
            }

            var authZModel = ScoreAuthorizationModel.From(score,score.CommentId);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, ScoreOperations.Delete))
            {
                return new HttpForbiddenResult("A DELETE request for deleting a score resource was attempted, but the authorization policy challenged the request.");
            }

            await PostService.DeleteScoreAsync(commentId,scoreId);

            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class PostResponseModel : ResourceResponseModel
    {
        public string Text { get; private set; }
        public string Username { get; private set; }
        public Guid UserId { get; private set; }
        public string TrackUrl { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyList<string> Tags { get; private set; }
        public string Genre { get; private set; }
        public IReadOnlyList<CommentResponseModel> Comments { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        private PostResponseModel(Guid id,Guid userId,string username, string text, string trackUrl, int likes, IReadOnlyList<string> tags,
            string genre,IReadOnlyList<CommentResponseModel> comments ,DateTime created, DateTime modified,string apiUrl)
            : base(GetHref(id, apiUrl), id)
        {
            Text = text;
            TrackUrl = trackUrl;
            UserId = userId;
            Username = username;
            Likes = likes;
            Tags = tags;
            Genre = genre;
            Comments = comments;
            Created = created;
            Modified = modified;

        }

        private static Uri GetHref(Guid id, string apiUrl)
        {
            return new Uri($"{apiUrl}/api/posts/{id}");
        } 

        internal static PostResponseModel From(Post post, string apiUrl)
        {
            string commentApiurl = GetHref(post.Id, apiUrl).AbsoluteUri;

            return new PostResponseModel(
                post.Id,
                post.UserId,
                post.Username,
                post.Text,
                post.TrackUrl,
                post.Likes,
                post.Tags,
                post.Genre.ToString(),
                post.Comments.Select(x => CommentResponseModel.From(x,commentApiurl)).ToList(),
                post.Created.ToDateTimeUtc(),
                post.Modified.ToDateTimeUtc(),
                apiUrl
                );
        }
    }
}

using System;
using System.Collections.Generic;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class PostResponseModel : ResourceResponseModel
    {
        public string Text { get; private set; }
        public string Username { get; private set; }
        public Guid UserId { get; private set; }
        public string TrackUrl { get; private set; }
        public IReadOnlyList<string> Tags { get; private set; }
        public string Genre { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        [Excludable]
        public PostSummaryResponseModel PostSummary { get; private set; }
        [Excludable]
        public CreditsResponseModel UserSummary { get; private set; }

        private PostResponseModel(Guid id, Guid userId, string username, string text, string trackUrl, IReadOnlyList<string> tags, string genre, DateTime created, DateTime modified, PostSummaryResponseModel summary, string apiUrl)
            : base(GetHref(id, apiUrl), id)
        {
            Text = text;
            TrackUrl = trackUrl;
            UserId = userId;
            Username = username;
            Tags = tags;
            Genre = genre;
            Created = created;
            Modified = modified;
            PostSummary = summary;

        }

        private static Uri GetHref(Guid id, string apiUrl)
        {
            return new Uri($"{apiUrl}/api/posts/{id}");
        } 

        internal static PostResponseModel From(Post post, PostSummary summary, string apiUrl)
        {
            //string commentApiurl = GetHref(post.Id, apiUrl).AbsoluteUri;

            return new PostResponseModel(
                post.Id,
                post.UserId,
                post.Username,
                post.Text,
                post.TrackUrl,
                post.Tags,
                post.Genre.ToString(),
                post.Created.ToDateTimeUtc(),
                post.Modified.ToDateTimeUtc(),
                PostSummaryResponseModel.From(summary,apiUrl),
                apiUrl
                );
        }
    }
}

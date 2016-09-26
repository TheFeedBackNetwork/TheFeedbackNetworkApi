using System;
using System.Collections.Generic;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class PostResponseModel : ResourceResponseModel
    {
        public string Text { get; private set; }
        public string TrackUrl { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyList<string> Tags { get; private set; }
        public GenreEnumModel Genre { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        private PostResponseModel(Guid id, string text, string trackUrl, int likes, IReadOnlyList<string> tags,
            GenreEnumModel genre, string apiUrl)
            : base(GetHref(id, apiUrl), id)
        {
            
        }

        private static Uri GetHref(Guid id, string apiUrl)
        {
            return new Uri(String.Format("{0}/api/posts/{1}", apiUrl, id));
        }

        /*internal static PostResponseModel From(Post post, string apiUrl)
        {
            
        }*/
    }
}

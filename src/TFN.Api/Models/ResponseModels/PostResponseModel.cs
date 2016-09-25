using System;
using System.Collections.Generic;

namespace TFN.Api.Models.ResponseModels
{
    public class PostResponseModel
    {
        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public string TrackUrl { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyList<string> Tags { get; private set; }
        public GenreEnumModel Genre { get; private set; }
        public string Created { get; private set; }
        public string Modified { get; private set; }
    }
}

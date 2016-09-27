using System;
using Newtonsoft.Json;

namespace TFN.Api.Models.Base
{
    public abstract class ResourceResponseModel
    {
        [JsonProperty(Order = -2)]
        public Guid Id { get; private set; }

        [JsonProperty(Order = -1)]
        public string Href { get; private set; }

        protected ResourceResponseModel(Uri href, Guid id)
            : this(href.AbsoluteUri, id)
        {
        }

        protected ResourceResponseModel(string href, Guid id)
        {
            Href = href;
            Id = id;
        }
    }
}

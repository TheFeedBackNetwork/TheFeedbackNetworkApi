using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TFN.Api.Models.Base
{
    public abstract class ResourceResponseModel
    {
        /// <summary>
        /// The identifier of the resource.
        /// </summary>
        [JsonProperty(Order = -2)]
        public Guid Id { get; private set; }

        /// <summary>
        /// The href pointing to the resource.
        /// </summary>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Api.Models.Base;

namespace TFN.Api.Models.ResponseModels
{
    public class CollapsedResourceModel : ResourceResponseModel
    {
        public CollapsedResourceModel(string href, Guid id)
            : base(href, id)
        {
        }

        public static CollapsedResourceModel From(ResourceResponseModel resource)
        {
            return new CollapsedResourceModel(resource.Href, resource.Id);
        }
    }
}

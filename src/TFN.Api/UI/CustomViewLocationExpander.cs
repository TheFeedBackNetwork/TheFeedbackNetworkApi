using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace TheFeedBackNetworkApi.UI
{
    public class CustomViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            yield return "~/ui/{1}/Views/{0}.cshtml";
            yield return "~/ui/SharedViews/{0}.cshtml";
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}

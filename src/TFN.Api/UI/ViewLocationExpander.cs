using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace TFN.Api.UI
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            yield return "~/UI/{1}/Views/{0}.cshtml";
            yield return "~/UI/Shared/{0}.cshtml";
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
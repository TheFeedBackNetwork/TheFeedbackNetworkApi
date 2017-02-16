using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TFN.Api.UI.TagHelpers
{
    [HtmlTargetElement(Attributes = "hide-if")]
    public class HideIfTagHelper : TagHelper
    {
        [HtmlAttributeName("hide-if")]
        public bool HideIf { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (HideIf)
            {
                output.SuppressOutput();
            }
        }
    }

    [HtmlTargetElement(Attributes = "hide-if-null")]
    public class HideIfNullTagHelper : TagHelper
    {
        [HtmlAttributeName("hide-if-null")]
        public object HideIfNull { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (HideIfNull == null)
            {
                output.SuppressOutput();
            }
        }
    }
}
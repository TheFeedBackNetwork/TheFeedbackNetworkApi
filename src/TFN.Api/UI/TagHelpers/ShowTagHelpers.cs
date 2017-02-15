using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TFN.Api.UI.TagHelpers
{
    [HtmlTargetElement(Attributes = "show-if")]
    public class ShowIfTagHelper : TagHelper
    {
        [HtmlAttributeName("show-if")]
        public bool ShowIf { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ShowIf == false)
            {
                output.SuppressOutput();
            }
        }
    }

    [HtmlTargetElement(Attributes = "show-if-null")]
    public class ShowifNullTagHelper : TagHelper
    {
        [HtmlAttributeName("show-if-null")]
        public object ShowIfNull { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ShowIfNull != null)
            {
                output.SuppressOutput();
            }
        }
    }
}
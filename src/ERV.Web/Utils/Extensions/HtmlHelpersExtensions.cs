using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ERV.Web.Utils.Extensions
{
    public static class HtmlHelpersExtensions
    {
        public static string Script(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return string.Empty;
        }

        public static string RenderPartialViewScripts(this IHtmlHelper htmlHelper)
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        htmlHelper.ViewContext.Writer.Write(template(null));
                    }
                }
            }
            return string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.CustomHtmlHelpers
{   // Създаване на HTML  таг с атрибути, който да се ползва като Helper във вютата
    public static class CustomHtmlHelpers
    { public static IHtmlString Image (this HtmlHelper helper,string src, string atr)
        {
            TagBuilder tb = new TagBuilder("img");
            //  tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("src", src);
            tb.Attributes.Add("atr", atr);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
    }
}
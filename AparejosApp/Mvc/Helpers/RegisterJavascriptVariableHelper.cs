using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AparejosApp.Mvc.Helpers
{
    using System.Web.Mvc;
    using Extensions;
    public static class RegisterJavascriptVariableHelper
    {

        public static MvcHtmlString RegisterJavascriptVariable(this HtmlHelper Helper, string name, object value)
        {
            return GetHtmlContent(name, value);
        }

        private static MvcHtmlString GetHtmlContent(string name, object value)
        {
            var js = string.Format("{0} = {1};", name, value.ToJson());
            return new MvcHtmlString(js);
        }

    }
}
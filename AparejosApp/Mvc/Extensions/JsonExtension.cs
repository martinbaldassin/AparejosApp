using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AparejosApp.Mvc.Extensions
{
    using System.IO;
    using Newtonsoft.Json;

    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            JsonSerializer js = JsonSerializer.Create(
                new JsonSerializerSettings()
                );

            var jw = new StringWriter();
            js.Serialize(jw, obj);
            return jw.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
namespace iqoptionapi.extensions
{
    internal static class JsonMixins
    {
        public static string AsJson(this object This)
        {
            return JsonConvert.SerializeObject(This);
        }

        public static T JsonAs<T>(this string This)
        {
            return JsonConvert.DeserializeObject<T>(This);
        }
    }
}

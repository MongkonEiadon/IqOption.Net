using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("IqOptionApi.Tests")]
namespace IqOptionApi.Extensions {
    public static class JsonExtensions {
        public static string AsJson(this object This) {
            return JsonConvert.SerializeObject(This);
        }

        public static T JsonAs<T>(this string This) {
            return JsonConvert.DeserializeObject<T>(This);
        }

        public static bool TryParseJson<T>(this string This, out T value) {
            try {
                value = This.JsonAs<T>();
                return true;
            }
            catch (Exception) {
                value = default(T);
            }

            return false;
        }
    }
}
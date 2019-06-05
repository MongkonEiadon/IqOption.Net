using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("IqOptionApi.Tests")]
namespace IqOptionApi.Extensions {
    internal static class JsonExtensions {
        internal static string AsJson(this object This) {
            return JsonConvert.SerializeObject(This);
        }

        internal static T JsonAs<T>(this string This) {
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
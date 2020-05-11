﻿using System;
using Newtonsoft.Json;

namespace IqOptionApi.extensions
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

        public static object JsonAs(this string This, Type type)
        {
            return JsonConvert.DeserializeObject(This, type);
        }

        public static bool TryParseJson<T>(this string This, out T value)
        {
            try
            {
                value = This.JsonAs<T>();
                return true;
            }
            catch (Exception)
            {
                value = default;
            }

            return false;
        }
    }

    public static class StringExtensions
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}
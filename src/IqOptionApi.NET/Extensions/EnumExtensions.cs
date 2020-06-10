using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace IqOptionApi.Extensions
{
    public static class EnumExtensions
    {
       public static string GetEnumMemberValue<T>(this T value)
            where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
       
       public static T ToEnum<T>(this string str)
       {
           var enumType = typeof(T);
           foreach (var name in Enum.GetNames(enumType))
           {
               var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
               if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
           }
           //throw exception or whatever handling you want or
           return default(T);
       }
    }
}
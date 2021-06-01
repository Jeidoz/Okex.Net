using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace CustomCexWrapper.Helpers
{
    public static class EnumExtensions
    {
        public static string ToValidApiValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (EnumMemberAttribute)fieldInfo.GetCustomAttribute(typeof(EnumMemberAttribute));
            return attribute.Value;
        }
    }
}
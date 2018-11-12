using System;
using System.Collections.Generic;
using ApplicationLogic.Model;

namespace ApplicationLogic.TypeConverter
{
    public class TypeToStringConverter
    {
        public static readonly Dictionary<Type, string> Map = new Dictionary<Type, string>()
        {
            { typeof(TypeNodeItem), "Type" },
            { typeof(PropertyNodeItem), "Property" },
            { typeof(ParameterNodeItem), "Parameter" },
            { typeof(NamespaceNodeItem), "Namespace" },
            { typeof(MethodNodeItem), "Method" },
            { typeof(AssemblyNodeItem), "Assembly" },
        };

        public static string GetStringFromType(object value)
        {
            var test = value.GetType();
            if (value != null && TypeToStringConverter.Map.TryGetValue(value.GetType(), out string converted))
            {
                return $"{converted}: ";
            }

            return "Unknown";
        }
    }
}
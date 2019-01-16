using System;
using System.Collections.Generic;
using ApplicationLogic.Model;

namespace Console_UI.TypeConverter
{
    public class TypeToStringConverter
    {
        public static readonly Dictionary<Type, string> Map = new Dictionary<Type, string>()
        {
            { typeof(TypeNodeItem), "Type" },
            { typeof(AttributeNodeItem), "Attribute" },
            { typeof(DerivedTypeNodeItem), "BaseType" },
            { typeof(ImplementedInterfaceNodeItem), "ImplementedInterface" },
            { typeof(PropertyNodeItem), "Property" },
            { typeof(ParameterNodeItem), "Parameter" },
            { typeof(NamespaceNodeItem), "Namespace" },
            { typeof(MethodNodeItem), "Method" },
            { typeof(AssemblyNodeItem), "Assembly" },
        };

        public static string GetStringFromType(object value)
        {
            Type test = value.GetType();
            if (value != null && TypeToStringConverter.Map.TryGetValue(value.GetType(), out string converted))
            {
                return $"{converted}: ";
            }

            return "Unknown";
        }
    }
}
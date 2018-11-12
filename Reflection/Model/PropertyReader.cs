using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection.Model
{
    public class PropertyReader
    {
        public string Name { get; set; }

        public TypeReader Type { get; set; }

        public PropertyReader(string name, TypeReader propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        public static List<PropertyReader> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyReader(t.Name, TypeReader.EmitReference(t.PropertyType))).ToList();
        }
    }
}

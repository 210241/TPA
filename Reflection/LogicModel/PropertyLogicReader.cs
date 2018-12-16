using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base.Model;

namespace Reflection.LogicModel
{
    public class PropertyLogicReader
    {
        public string Name { get; set; }

        public TypeLogicReader Type { get; set; }

        private PropertyLogicReader()
        {

        }

        public PropertyLogicReader(string name, TypeLogicReader propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        public PropertyLogicReader(PropertyBase baseProperty)
        {
            Name = baseProperty.Name;
            Type = TypeLogicReader.GetOrAdd(baseProperty.Type);
        }

        public static List<PropertyLogicReader> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyLogicReader(t.Name, TypeLogicReader.GetOrAdd(t.PropertyType))).ToList();
        }
    }
}
using System.Collections.Generic;
using System.Reflection;
using DataTransfer.Model;
using Reflection.ExtensionMethods;

namespace Reflection
{
    public partial class Reflection
    {
        private IEnumerable<PropertyData> EmitProperties(IEnumerable<PropertyInfo> props, AssemblyDataStorage dataStore)
        {
            List<PropertyData> properties = new List<PropertyData>();
            foreach (PropertyInfo property in props)
            {
                if (property.GetGetMethod().IsVisible() || property.GetSetMethod().IsVisible())
                {
                    string id = $"{property.DeclaringType.FullName}.{property.Name}";
                    if (dataStore.PropertiesDictionary.ContainsKey(id))
                    {
                        _logger.Trace("Using property already added to dictionary: Id =" + id);
                        properties.Add(dataStore.PropertiesDictionary[id]);
                    }
                    else
                    {
                        PropertyData newProperty = new PropertyData()
                        {
                            Id = id,
                            Name = property.Name
                        };
                        _logger.Trace("Adding new property to dictionary: " + newProperty.Id +" ;Name = " + newProperty.Name);
                        dataStore.PropertiesDictionary.Add(newProperty.Id, newProperty);
                        properties.Add(newProperty);

                        newProperty.TypeMetadata = LoadTypeData(property.PropertyType, dataStore);
                    }
                }
            }

            return properties;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DataTransfer.Model;

namespace Reflection
{
    public partial class Reflection
    {
        private NamespaceData LoadNamespaceData(string name, IEnumerable<Type> types, AssemblyDataStorage dataStore)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(name)} is null/empty/whitespace");
            }

            NamespaceData namespaceData = new NamespaceData()
            {
                Id = name,
                Name = name
            };

            _logger.Trace("Adding Namespace to dictionary: " + namespaceData.Name);
            dataStore.NamespacesDictionary.Add(namespaceData.Name, namespaceData);

            namespaceData.Types = (from type in types orderby type.Name select LoadTypeData(type, dataStore)).ToList();

            return namespaceData;
        }
    }
}
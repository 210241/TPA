using System;
using System.Linq;
using System.Reflection;
using DataTransfer.Model;
using Reflection.ExtensionMethods;

namespace Reflection
{
    public partial class Reflection
    {
        private AssemblyDataStorage LoadAssemblyData(Assembly assembly)
        {
            AssemblyData assemblyData = new AssemblyData()
            {
                Id = assembly.ManifestModule.FullyQualifiedName,
                Name = assembly.ManifestModule.Name,               
            };

            AssemblyDataStorage dataStore = new AssemblyDataStorage(assemblyData);

            assemblyData.Namespaces = (from Type type in assembly.GetTypes()
                where type.IsVisible()
                group type by type.GetNamespace() into namespaceGroup
                orderby namespaceGroup.Key
                select LoadNamespaceData(namespaceGroup.Key, namespaceGroup, dataStore)).ToList();

            return dataStore;
        }
    }
}
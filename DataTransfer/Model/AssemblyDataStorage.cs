using System;
using System.Collections.Generic;

namespace DataTransfer.Model
{
    public class AssemblyDataStorage
    {
        public AssemblyData AssemblyData { get; }

        public Dictionary<string, NamespaceData> NamespacesDictionary { get; }

        public Dictionary<string, TypeData> TypesDictionary { get; }

        public Dictionary<string, PropertyData> PropertiesDictionary { get; }

        public Dictionary<string, MethodData> MethodsDictionary { get; }

        public Dictionary<string, ParameterData> ParametersDictionary { get; }

        public AssemblyDataStorage(AssemblyData assemblyData)
        {
            AssemblyData = assemblyData ?? throw new ArgumentNullException(nameof(assemblyData));
            NamespacesDictionary = new Dictionary<string, NamespaceData>();
            TypesDictionary = new Dictionary<string, TypeData>();
            PropertiesDictionary = new Dictionary<string, PropertyData>();
            MethodsDictionary = new Dictionary<string, MethodData>();
            ParametersDictionary = new Dictionary<string, ParameterData>();
        }
    }
}
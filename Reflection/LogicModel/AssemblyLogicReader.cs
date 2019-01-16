using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base.Model;

namespace Reflection.LogicModel
{
    public class AssemblyLogicReader
    {
        public List<NamespaceLogicReader> NamespaceLogicReader { get; set; }

        public string Name { get; set; }

        private AssemblyLogicReader()
        {

        }

        public AssemblyLogicReader(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceLogicReader = types.GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceLogicReader(t.Key, t.ToList())).ToList();
        }

        public AssemblyLogicReader(AssemblyBase assemblybase)
        {
            this.Name = assemblybase.Name;
            this.NamespaceLogicReader = assemblybase.Namespaces?.Select(ns => new NamespaceLogicReader(ns)).ToList();
        }

        
    }
}

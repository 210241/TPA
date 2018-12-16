using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base.Model;
using Reflection.Model;

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
            NamespaceLogicReader = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceLogicReader(t.Key, t.ToList())).ToList();
        }

        public AssemblyLogicReader(AssemblyReader assemblyReader)
        {
            this.Name = assemblyReader.Name;
            this.NamespaceLogicReader = new List<NamespaceLogicReader>();
            foreach (var baseElem in assemblyReader.Namespaces)
            {
                NamespaceLogicReader.Add(new NamespaceLogicReader(baseElem));
            }
        }

        
    }
}

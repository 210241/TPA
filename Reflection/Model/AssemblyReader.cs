using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base.Model;


namespace Reflection.Model
{
    [DataContract(Name = "AssemblyReader")]
    public class AssemblyReader : AssemblyBase
    {
        [DataMember]
        public List<NamespaceReader> NamespaceReader { get; set; }

        [DataMember]
        public string Name { get; set; }

        private AssemblyReader()
        {

        }

        public AssemblyReader(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceReader = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceReader(t.Key, t.ToList())).ToList();
        }
    }
}

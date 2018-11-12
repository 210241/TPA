using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Reflection.Model
{
    public class AssemblyReader
    {
        public List<NamespaceReader> NamespaceReader { get; set; }

        public string Name { get; set; }

        public AssemblyReader(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceReader = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceReader(t.Key, t.ToList())).ToList();
        }
    }
}

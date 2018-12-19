using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Base.Model;

namespace Reflection.LogicModel
{
    public class NamespaceLogicReader
    {

        public string Name { get; set; }

        public List<TypeLogicReader> Types { get; set; }

        private NamespaceLogicReader()
        {

        }

        public NamespaceLogicReader(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => new TypeLogicReader(t)).ToList();
        }

        public NamespaceLogicReader(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            this.Types = namespaceBase.Types?.Select(t => TypeLogicReader.GetOrAdd(t)).ToList();
        }
    }
}
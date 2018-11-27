using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Reflection.Model
{
    [DataContract(Name = "NamespaceReader")]
    public class NamespaceReader
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeReader> Types { get; set; }

        private NamespaceReader()
        {

        }

        public NamespaceReader(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => new TypeReader(t)).ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection.Model
{

    public class NamespaceReader
    {

        public string Name { get; set; }

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

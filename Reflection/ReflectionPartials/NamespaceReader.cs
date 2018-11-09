using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection.ReflectionPartials
{

    public class NamespaceReader
    {

        public string Name { get; set; }

        public List<TypeReader> Types { get; set; }

        public NamespaceReader(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => new TypeReader(t)).ToList();
        }

    }
}

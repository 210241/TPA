using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model
{
    public abstract class NamespaceBase
    {
        public string Name { get; set; }

        public List<TypeBase> Types { get; set; }
    }
}

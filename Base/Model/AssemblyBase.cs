using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model
{
    public abstract class  AssemblyBase
    {   
        public List<NamespaceBase> Namespaces { get; set; }
        public string Name { get; set; }
    }
}

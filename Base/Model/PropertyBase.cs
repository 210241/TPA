using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model
{
    public abstract class PropertyBase
    {
        public string Name { get; set; }

        public TypeBase Type { get; set; }
    }
}

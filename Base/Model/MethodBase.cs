using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Enums;

namespace Base.Model
{
   public abstract class MethodBase
    {
        public string Name { get; set; }

        public List<TypeBase> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public VirtualEnum VirtualEnum { get; set; }

        public TypeBase ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterBase> Parameters { get; set; }
    }
}

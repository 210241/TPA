using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Enums;

namespace Base.Model
{
    public abstract class TypeBase
    {

        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeBase BaseType { get; set; }

        public List<TypeBase> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public SealedEnum SealedEnum { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeBase> ImplementedInterfaces { get; set; }

        public List<TypeBase> NestedTypes { get; set; }

        public List<PropertyBase> Properties { get; set; }

        public TypeBase DeclaringType { get; set; }

        public List<MethodBase> Methods { get; set; }

        public List<MethodBase> Constructors { get; set; }

        public List<ParameterBase> Fields { get; set; }
    }
}

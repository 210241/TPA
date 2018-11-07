using System;
using System.Collections.Generic;
using DataTransfer.Model.Enums;

namespace DataTransfer.Model
{
    public class TypeData : BaseData
    {
        public string NamespaceName { get; set; }

        public TypeData BaseType { get; set; }

        public IEnumerable<TypeData> GenericArguments { get; set; }

        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }

        public TypeKind TypeKind { get; set; }

        public IEnumerable<Attribute> Attributes { get; set; }

        public IEnumerable<TypeData> ImplementedInterfaces { get; set; }

        public IEnumerable<TypeData> NestedTypes { get; set; }

        public IEnumerable<PropertyData> Properties { get; set; }

        public TypeData DeclaringType { get; set; }

        public IEnumerable<MethodData> Methods { get; set; }

        public IEnumerable<MethodData> Constructors { get; set; }
    }
}
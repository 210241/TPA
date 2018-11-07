using System;
using System.Collections.Generic;
using DataTransfer.Model.Enums;

namespace DataTransfer.Model
{
    public class MethodData : BaseData
    {
        public IEnumerable<TypeData> GenericArguments { get; set; }

        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        public TypeData ReturnType { get; set; }

        public bool Extension { get; set; }

        public IEnumerable<ParameterData> Parameters { get; set; }
    }
}
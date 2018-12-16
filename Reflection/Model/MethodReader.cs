using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Base.Model;
using Base.Enums;
using Reflection.LogicModel;


namespace Reflection.Model
{
    public class MethodReader : Base.Model.MethodBase
    {
        public MethodReader(Base.Model.MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeReader.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;
            
            GenericArguments = baseMethod.GenericArguments.Select(t => TypeReader.GetOrAdd(t) as TypeBase).ToList();

            Parameters = baseMethod.Parameters.Select(t => new ParameterReader(t) as ParameterBase).ToList();
        }

        public MethodReader(MethodLogicReader baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeReader.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments.Select(t => TypeReader.GetOrAdd(t) as TypeBase).ToList();

            Parameters = baseMethod.Parameters.Select(t => new ParameterReader(t) as ParameterBase).ToList();
        }

    }
}

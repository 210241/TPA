using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Base.Enums;

namespace Reflection.LogicModel
{
    public class MethodLogicReader
    {
        public string Name { get; set; }

        public List<TypeLogicReader> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public VirtualEnum VirtualEnum { get; set; }

        public TypeLogicReader ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterLogicReader> Parameters { get; set; }

        private MethodLogicReader()
        {

        }

        public MethodLogicReader(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? new List<TypeLogicReader>() : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodLogicReader(Base.Model.MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeLogicReader.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;
            foreach (var baseGenericArgument in baseMethod.GenericArguments)
            {
                this.GenericArguments.Add(TypeLogicReader.GetOrAdd(baseGenericArgument));
            }

            foreach (var baseParameter in baseMethod.Parameters)
            {
                this.Parameters.Add(new ParameterLogicReader(baseParameter));
            }
        }

        private List<TypeLogicReader> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => new TypeLogicReader(t)).ToList();
        }

        public static List<MethodLogicReader> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodLogicReader(t)).ToList();
        }

        private static List<ParameterLogicReader> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterLogicReader(t.Name, TypeLogicReader.GetOrAdd(t.ParameterType))).ToList();
        }

        private static TypeLogicReader EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            TypeLogicReader.StoreType(methodInfo.ReturnType);
            return TypeLogicReader.GetOrAdd(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private void EmitModifiers(MethodBase method)
        {
            AccessLevel = method.IsPublic ? AccessLevel.IsPublic :
                method.IsFamily ? AccessLevel.IsProtected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.IsPrivate;

            AbstractEnum = method.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;

            StaticEnum = method.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;

            VirtualEnum = method.IsVirtual ? VirtualEnum.Virtual : VirtualEnum.NotVirtual;

            //return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, _abstract, _static, _virtual);
        }

        public static List<MethodLogicReader> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodLogicReader(t)).ToList();
        }
    }
}
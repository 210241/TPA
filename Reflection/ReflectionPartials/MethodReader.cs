using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataTransfer.Model.Enums;

namespace Reflection.ReflectionPartials
{

    public class MethodReader
    {
        public string Name { get; set; }

        public List<TypeReader> GenericArguments { get; set; }

        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }


        public TypeReader ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterReader> Parameters { get; set; }

        public MethodReader(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        private List<TypeReader> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => new TypeReader(t)).ToList();
        }

        public static List<MethodReader> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodReader(t)).ToList();
        }

        private static List<ParameterReader> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterReader(t.Name, TypeReader.EmitReference(t.ParameterType))).ToList();
        }

        private static TypeReader EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            TypeReader.StoreType(methodInfo.ReturnType);
            return TypeReader.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = method.IsPublic ? AccessLevel.IsPublic :
                method.IsFamily ? AccessLevel.IsProtected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.IsPrivate;

            AbstractEnum _abstract = method.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;

            StaticEnum _static = method.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;

            VirtualEnum _virtual = method.IsVirtual ? VirtualEnum.Virtual : VirtualEnum.NotVirtual;

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, _abstract, _static, _virtual);
        }

        public static List<MethodReader> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodReader(t)).ToList();
        }
    }
}

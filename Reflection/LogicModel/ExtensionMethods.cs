using System;
using System.Reflection;
using Base.Enums;
using AbstractEnum = Reflection.Enums.AbstractEnum;

namespace Reflection.LogicModel
{
    public static class ExtensionMethods
    {
        internal static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }

        internal static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }

        internal static string GetNamespace(this Type type)
        {
            string ns = type.Namespace;
            return ns ?? string.Empty;
        }

        internal static Reflection.Enums.AbstractEnum toReflectionEnum(this Base.Enums.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.AbstractEnum.Abstract:
                    return Reflection.Enums.AbstractEnum.Abstract;

                case Base.Enums.AbstractEnum.NotAbstract:
                    return Reflection.Enums.AbstractEnum.NotAbstract;
                default:
                     throw new Exception();
            }
        }

        internal static Reflection.Enums.AccessLevel toReflectionEnum(this Base.Enums.AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.AccessLevel.Default:
                    return Reflection.Enums.AccessLevel.Default;

                case Base.Enums.AccessLevel.Internal:
                    return Reflection.Enums.AccessLevel.Internal;

                case Base.Enums.AccessLevel.IsPrivate:
                    return Reflection.Enums.AccessLevel.IsPrivate;

                case Base.Enums.AccessLevel.IsProtected:
                    return Reflection.Enums.AccessLevel.IsProtected;

                case Base.Enums.AccessLevel.IsProtectedInternal:
                    return Reflection.Enums.AccessLevel.IsProtectedInternal;

                case Base.Enums.AccessLevel.IsPublic:
                    return Reflection.Enums.AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static Reflection.Enums.SealedEnum toReflectionEnum(this Base.Enums.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.SealedEnum.NotSealed:
                    return Reflection.Enums.SealedEnum.NotSealed;

                case Base.Enums.SealedEnum.Sealed:
                    return Reflection.Enums.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Reflection.Enums.StaticEnum toReflectionEnum(this Base.Enums.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.StaticEnum.Static:
                    return Reflection.Enums.StaticEnum.Static;

                case Base.Enums.StaticEnum.NotStatic:
                    return Reflection.Enums.StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static Reflection.Enums.TypeKind toReflectionEnum(this Base.Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.TypeKind.ClassType:
                    return Reflection.Enums.TypeKind.ClassType;
                case Base.Enums.TypeKind.EnumType:
                    return Reflection.Enums.TypeKind.EnumType;
                case Base.Enums.TypeKind.InterfaceType:
                    return Reflection.Enums.TypeKind.InterfaceType;
                case Base.Enums.TypeKind.StructType:
                    return Reflection.Enums.TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static Reflection.Enums.VirtualEnum toReflectionEnum(this Base.Enums.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.VirtualEnum.NotVirtual:
                    return Reflection.Enums.VirtualEnum.NotVirtual;

                case Base.Enums.VirtualEnum.Virtual:
                    return Reflection.Enums.VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}

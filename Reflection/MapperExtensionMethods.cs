using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Base
{
    public static class MapperExtensionMethods
    {
        internal static Base.Enums.AbstractEnum toBaseEnum(this Reflection.Enums.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.AbstractEnum.Abstract:
                    return Base.Enums.AbstractEnum.Abstract;
              
                case Reflection.Enums.AbstractEnum.NotAbstract:
                    return Base.Enums.AbstractEnum.NotAbstract;
                default:
                    throw new System.Exception();
            }
        }

        internal static Base.Enums.AccessLevel toBaseEnum(this Reflection.Enums.AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.AccessLevel.Default:
                    return Base.Enums.AccessLevel.Default;

                case Reflection.Enums.AccessLevel.Internal:
                    return Base.Enums.AccessLevel.Internal;

                case Reflection.Enums.AccessLevel.IsPrivate:
                    return Base.Enums.AccessLevel.IsPrivate;

                case Reflection.Enums.AccessLevel.IsProtected:
                    return Base.Enums.AccessLevel.IsProtected;

                case Reflection.Enums.AccessLevel.IsProtectedInternal:
                    return Base.Enums.AccessLevel.IsProtectedInternal;

                case Reflection.Enums.AccessLevel.IsPublic:
                    return Base.Enums.AccessLevel.IsPublic;

                default:
                    throw new System.Exception();
            }
        }

        internal static Base.Enums.SealedEnum toBaseEnum(this Reflection.Enums.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.SealedEnum.NotSealed:
                    return Base.Enums.SealedEnum.NotSealed;

                case Reflection.Enums.SealedEnum.Sealed:
                    return Base.Enums.SealedEnum.Sealed;
                default:
                    throw new System.Exception();
            }
        }

        internal static Base.Enums.StaticEnum toBaseEnum(this Reflection.Enums.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.StaticEnum.Static:
                    return Base.Enums.StaticEnum.Static;

                case Reflection.Enums.StaticEnum.NotStatic:
                    return Base.Enums.StaticEnum.NotStatic;
                default:
                    throw new System.Exception();
            }
        }

        internal static Base.Enums.TypeKind toBaseEnum(this Reflection.Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.TypeKind.ClassType:
                    return Base.Enums.TypeKind.ClassType;
                case Reflection.Enums.TypeKind.EnumType:
                    return Base.Enums.TypeKind.EnumType;
                case Reflection.Enums.TypeKind.InterfaceType:
                    return Base.Enums.TypeKind.InterfaceType;
                case Reflection.Enums.TypeKind.StructType:
                    return Base.Enums.TypeKind.StructType;

                default:
                    throw new System.Exception();
            }
        }
        internal static Base.Enums.VirtualEnum toBaseEnum(this Reflection.Enums.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.VirtualEnum.NotVirtual:
                    return Base.Enums.VirtualEnum.NotVirtual;

                case Reflection.Enums.VirtualEnum.Virtual:
                    return Base.Enums.VirtualEnum.Virtual;
                default:
                    throw new System.Exception();
            }
        }
    }
}

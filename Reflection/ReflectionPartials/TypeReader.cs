using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataTransfer.Model;
using DataTransfer.Model.Enums;
using Reflection.ExtensionMethods;

namespace Reflection
{
    public partial class Reflection
    {
        private TypeData LoadTypeData(Type type, AssemblyDataStorage dataStore)
        {
            if (type == null)
            {
                throw new ArgumentNullException($"{nameof(type)} argument is null.");
            }

            if (type.IsGenericParameter)
            {
                return LoadGenericParameterTypeObject(type, dataStore);

            }
            else
            {
                if (!dataStore.TypesDictionary.ContainsKey(type.FullName))
                {
                    TypeData dataType;

                    // if type is not declared in assembly being inspected
                    if (type.Assembly.ManifestModule.FullyQualifiedName != dataStore.AssemblyData.Id) // load basic info
                    {
                        dataType = LoadSimpleTypeObject(type, dataStore);
                    }
                    else // load full type information
                    {
                        dataType = LoadFullTypeObject(type, dataStore);
                    }

                    return dataType;
                }
                else
                {
                    _logger.Trace("Using type already added to dictionary with key: " + type.FullName);
                    return dataStore.TypesDictionary[type.FullName];

                }
            }
        }

        private TypeData LoadGenericParameterTypeObject(Type type, AssemblyDataStorage dataStore)
        {
            TypeData dataType;
            string id = $"{type.DeclaringType.FullName}<{type.Name}>";

            _logger.Trace("Adding generic argument type with Id =" + id);

            if (!dataStore.TypesDictionary.ContainsKey(id))
            {
                dataType = new TypeData()
                {
                    Id = id,
                    Name = type.Name,
                    NamespaceName = type.Namespace,
                    Modifiers = null,
                    TypeKind = GetTypeKind(type),
                    Attributes = new List<Attribute>(),
                    Properties = new List<PropertyData>(),
                    Constructors = new List<MethodData>(),
                    ImplementedInterfaces = new List<TypeData>(),
                    Methods = new List<MethodData>(),
                    NestedTypes = new List<TypeData>(),
                    GenericArguments = new List<TypeData>()
                };
                dataStore.TypesDictionary.Add(dataType.Id, dataType);
                return dataType;
            }
            else
            {
                return dataStore.TypesDictionary[id];
            }
        }

        private TypeData LoadSimpleTypeObject(Type type, AssemblyDataStorage dataStore)
        {
            TypeData dataType;
            dataType = new TypeData // add only basic information
            {
                Id = type.FullName,
                Name = type.Name,
                NamespaceName = type.Namespace,
                Modifiers = EmitModifiers(type),
                TypeKind = GetTypeKind(type),
                Attributes = type.GetCustomAttributes(false).Cast<Attribute>(),
                Properties = new List<PropertyData>(),
                Constructors = new List<MethodData>(),
                ImplementedInterfaces = new List<TypeData>(),
                Methods = new List<MethodData>(),
                NestedTypes = new List<TypeData>()
            };

            dataStore.TypesDictionary.Add(type.FullName, dataType);

            if (type.IsGenericTypeDefinition)
            {
                dataType.GenericArguments = EmitGenericArguments(type.GetGenericArguments(), dataStore);
                dataType.Name =
                    $"{type.Name}<{dataType.GenericArguments.Select(a => a.Name).Aggregate((p, n) => $"{p}, {n}")}>";
            }
            else
            {
                dataType.GenericArguments = new List<TypeData>();
            }

            _logger.Trace("Adding type not declared in assembly being inspected: Id =" + dataType.Id + " ; Name = " + dataType.Name);
            return dataType;
        }

        private TypeData LoadFullTypeObject(Type type, AssemblyDataStorage dataStore)
        {
            TypeData dataType = new TypeData()
            {
                Id = type.FullName,
                Name = type.Name,
                NamespaceName = type.Namespace,
                Modifiers = EmitModifiers(type),
                TypeKind = GetTypeKind(type),
                Attributes = type.GetCustomAttributes(false).Cast<Attribute>()
            };
            _logger.Trace("Adding type: Id =" + dataType.Id + " ; Name = " + dataType.Name);
            dataStore.TypesDictionary.Add(type.FullName, dataType);

            dataType.DeclaringType = EmitDeclaringType(type.DeclaringType, dataStore);
            dataType.ImplementedInterfaces = EmitImplements(type.GetInterfaces(), dataStore);
            dataType.BaseType = EmitExtends(type.BaseType, dataStore);
            dataType.NestedTypes = EmitNestedTypes(type.GetNestedTypes(), dataStore);

            if (type.IsGenericTypeDefinition)
            {
                dataType.GenericArguments = EmitGenericArguments(type.GetGenericArguments(), dataStore);
                dataType.Name =
                    $"{type.Name}<{dataType.GenericArguments.Select(a => a.Name).Aggregate((p, n) => $"{p}, {n}")}>";
            }
            else
            {
                dataType.GenericArguments = new List<TypeData>();
            }

            dataType.Constructors = EmitMethods(type.GetConstructors(), dataStore);
            dataType.Methods = EmitMethods(type.GetMethods(BindingFlags.DeclaredOnly), dataStore);

            dataType.Properties = EmitProperties(type.GetProperties(), dataStore);
            return dataType;
        }

        internal IEnumerable<TypeData> EmitGenericArguments(IEnumerable<Type> arguments, AssemblyDataStorage dataStore)
        {
            return (from Type argument in arguments select LoadTypeData(argument, dataStore)).ToList();
        }

        private TypeData EmitExtends(Type baseType, AssemblyDataStorage dataStore)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
            {
                return null;
            }

            return LoadTypeData(baseType, dataStore);
        }

        private TypeData EmitDeclaringType(Type declaringType, AssemblyDataStorage dataStore)
        {
            if (declaringType == null)
            {
                return null;
            }

            return LoadTypeData(declaringType, dataStore);
        }

        private IEnumerable<TypeData> EmitNestedTypes(IEnumerable<Type> nestedTypes, AssemblyDataStorage dataStore)
        {
            return (from type in nestedTypes
                    where type.IsVisible()
                    select LoadTypeData(type, dataStore)).ToList();
        }

        private IEnumerable<TypeData> EmitImplements(IEnumerable<Type> interfaces, AssemblyDataStorage dataStore)
        {
            return (from currentInterface in interfaces
                    select LoadTypeData(currentInterface, dataStore)).ToList();
        }

        private TypeKind GetTypeKind(Type type) // #80 TPA: Reflection - Invalid return value of GetTypeKind()
        {
            return type.IsEnum ? TypeKind.EnumType :
                type.IsValueType ? TypeKind.StructType :
                type.IsInterface ? TypeKind.InterfaceType :
                TypeKind.ClassType;
        }

        private Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevel accessLevel = AccessLevel.IsPrivate;
            // check if not default
            if (type.IsPublic)
            {
                accessLevel = AccessLevel.IsPublic;
            }
            else if (type.IsNestedPublic)
            {
                accessLevel = AccessLevel.IsPublic;
            }
            else if (type.IsNestedFamily)
            {
                accessLevel = AccessLevel.IsProtected;
            }
            else if (type.IsNestedFamANDAssem)
            {
                accessLevel = AccessLevel.IsProtectedInternal;
            }

            SealedEnum sealedEnum = SealedEnum.NotSealed;
            if (type.IsSealed)
            {
                sealedEnum = SealedEnum.Sealed;
            }

            AbstractEnum abstractEnum = AbstractEnum.NotAbstract;
            if (type.IsAbstract)
            {
                abstractEnum = AbstractEnum.Abstract;
            }

            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(accessLevel, sealedEnum, abstractEnum);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataTransfer.Model;
using DataTransfer.Model.Enums;
using Reflection.ExtensionMethods;

namespace Reflection
{
    public partial class Reflection
    {
        private MethodData LoadMethodData(MethodBase method, AssemblyDataStorage dataStore)
        {
            if (method == null)
            {
                throw new ArgumentNullException($"{nameof(method)} argument is null.");
            }

            MethodData methodData = new MethodData()
            {
                Name = method.Name,
                Modifiers = EmitModifiers(method),
                Extension = IsExtension(method)
            };

            methodData.GenericArguments = !method.IsGenericMethodDefinition ? new List<TypeData>() : EmitGenericArguments(method.GetGenericArguments(), dataStore);
            methodData.ReturnType = EmitReturnType(method, dataStore);
            methodData.Parameters = EmitParameters(method.GetParameters(), dataStore).ToList();

            string parameters = methodData.Parameters.Any()
                ? methodData.Parameters.Select(methodInstance => methodInstance.Name)
                    .Aggregate((current, next) => current + ", " + next)
                : "none";

            string generics = methodData.GenericArguments.Any()
                ? methodData.GenericArguments.Select(typeInstance => typeInstance.Id)
                    .Aggregate((c, n) => $"{c}, {n}")
                : "none";

            methodData.Id = $"{method.DeclaringType.FullName}{method.Name} args {parameters} generics {generics} declaredBy {method.DeclaringType.FullName}";

            if (!dataStore.MethodsDictionary.ContainsKey(methodData.Id))
            {
                _logger.Trace("Adding method to dictionary: Id =" + methodData.Id);
                dataStore.MethodsDictionary.Add(methodData.Id, methodData);
                return methodData;
            }
            else
            {
                _logger.Trace("Using method already added to dictionary: Id =" + methodData.Id);
                return dataStore.MethodsDictionary[methodData.Id];
            }
        }

        internal IEnumerable<MethodData> EmitMethods(IEnumerable<MethodBase> methods, AssemblyDataStorage dataStore)
        {
            return (from MethodBase currentMethod in methods
                    where currentMethod.IsVisible()
                    select LoadMethodData(currentMethod, dataStore)).ToList();
        }

        private IEnumerable<ParameterData> EmitParameters(IEnumerable<ParameterInfo> parameters, AssemblyDataStorage dataStore)
        {
            List<ParameterData> parametersData = new List<ParameterData>();
            foreach (var parameter in parameters)
            {
                string id = $"{parameter.ParameterType.FullName}.{parameter.Name}";
                if (dataStore.ParametersDictionary.ContainsKey(id))
                {
                    _logger.Trace("Using parameter already added to dictionary: Id =" + id);
                    parametersData.Add(dataStore.ParametersDictionary[id]);
                }
                else
                {
                    ParameterData newParameter = new ParameterData(parameter.Name, LoadTypeData(parameter.ParameterType, dataStore));
                    newParameter.Id = id;
                    dataStore.ParametersDictionary.Add(id, newParameter);
                    _logger.Trace("Adding parameter to dictionary: Id =" + id);
                    parametersData.Add(newParameter);
                }
            }

            return parametersData;
        }

        private TypeData EmitReturnType(MethodBase method, AssemblyDataStorage dataStore)
        {
            MethodInfo methodInfo = method as MethodInfo;
            return methodInfo == null ? null : LoadTypeData(methodInfo.ReturnType, dataStore);
        }

        private static bool IsExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = AccessLevel.IsPrivate;
            if (method.IsPublic)
            {
                access = AccessLevel.IsPublic;
            }
            else if (method.IsFamily)
            {
                access = AccessLevel.IsProtected;
            }
            else if (method.IsFamilyAndAssembly)
            {
                access = AccessLevel.IsProtectedInternal;
            }

            AbstractEnum isAbstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
            {
                isAbstract = AbstractEnum.Abstract;
            }

            StaticEnum isStatic = StaticEnum.NotStatic;
            if (method.IsStatic)
            {
                isStatic = StaticEnum.Static;
            }

            VirtualEnum isVirtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
            {
                isVirtual = VirtualEnum.Virtual;
            }

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, isAbstract, isStatic, isVirtual);
        }
    }
}
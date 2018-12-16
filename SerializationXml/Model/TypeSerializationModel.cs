using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Enums;
using Base.Model;

namespace SerializationXml.Model
{
    [DataContract(Name = "TypeSerializationModel", IsReference = true)]
    public class TypeSerializationModel : TypeBase
    {
        private TypeSerializationModel()
        {

        }

        private TypeSerializationModel(TypeBase baseType)
        {
            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.AbstractEnum;
            this.AccessLevel = baseType.AccessLevel;
            this.SealedEnum = baseType.SealedEnum;
            this.StaticEnum = baseType.StaticEnum;

            //foreach (var baseConstructor in baseType.Constructors)
            //{
            //    this.Constructors.Add(new MethodSerializationModel(baseConstructor));
            //}

            //foreach (var baseField in baseType.Fields)
            //{
            //    this.Fields.Add(new ParameterSerializationModel(baseField));
            //}

            //foreach (var baseGenericArgument in baseType.GenericArguments)
            //{
            //    this.GenericArguments.Add(GetOrAdd(baseGenericArgument));
            //}

            //foreach (var baseImplementedInterface in baseType.ImplementedInterfaces)
            //{
            //    this.ImplementedInterfaces.Add(GetOrAdd(baseImplementedInterface));
            //}

            //foreach (var baseMethod in baseType.Methods)
            //{
            //    this.Methods.Add(new MethodSerializationModel(baseMethod));
            //}

            //foreach (var baseNestedType in baseType.NestedTypes)
            //{
            //    this.NestedTypes.Add(GetOrAdd(baseNestedType));
            //}

            //foreach (var baseProperty in baseType.Properties)
            //{
            //    this.Properties.Add(new PropertySerializationModel(baseProperty));
            //}
            Constructors = baseType.Constructors.Select(t => new MethodSerializationModel(t)).ToList();

            Fields = baseType.Fields.Select(t => new ParameterSerializationModel(t)).ToList();

            GenericArguments = baseType.GenericArguments.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces.Select(GetOrAdd).ToList();

            Methods = baseType.Methods.Select(t => new MethodSerializationModel(t)).ToList();

            NestedTypes = baseType.NestedTypes.Select(GetOrAdd).ToList();

            Properties = baseType.Properties.Select(t => new PropertySerializationModel(t)).ToList();

        }

        public static TypeSerializationModel GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (TypeDictionary.ContainsKey(baseType.Name))
                {
                    return TypeDictionary[baseType.Name];
                }
                else
                {
                    return new TypeSerializationModel(baseType);
                }
            }
            else
                return null;
        }

        [DataMember]
        public new string Name { get; set; }

        [DataMember]
        public new string NamespaceName { get; set; }

        [DataMember]
        public new TypeSerializationModel BaseType { get; set; }

        [DataMember]
        public new List<TypeSerializationModel> GenericArguments { get; set; }

        [DataMember]
        public new AccessLevel AccessLevel { get; set; }

        [DataMember]
        public new AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public new StaticEnum StaticEnum { get; set; }

        [DataMember]
        public new SealedEnum SealedEnum { get; set; }

        [DataMember]
        public new TypeKind Type { get; set; }

        [DataMember]
        public new List<TypeSerializationModel> ImplementedInterfaces { get; set; }

        [DataMember]
        public new List<TypeSerializationModel> NestedTypes { get; set; }

        [DataMember]
        public new List<PropertySerializationModel> Properties { get; set; }

        [DataMember]
        public new TypeSerializationModel DeclaringType { get; set; }

        [DataMember]
        public new List<MethodSerializationModel> Methods { get; set; }

        [DataMember]
        public new List<MethodSerializationModel> Constructors { get; set; }

        [DataMember]
        public new List<ParameterSerializationModel> Fields { get; set; }

        public static Dictionary<string, TypeSerializationModel> TypeDictionary = new Dictionary<string, TypeSerializationModel>();

    }
}

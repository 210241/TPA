﻿using System;
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
    public class TypeSerializationModel
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

            Constructors = baseType.Constructors?.Select(t => new MethodSerializationModel(t)).ToList();

            Fields = baseType.Fields?.Select(t => new FieldSerializationModel(t)).ToList();

            GenericArguments = baseType.GenericArguments?.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces?.Select(GetOrAdd).ToList();

            Methods = baseType.Methods?.Select(t => new MethodSerializationModel(t)).ToList();

            NestedTypes = baseType.NestedTypes?.Select(GetOrAdd).ToList();

            Properties = baseType.Properties?.Select(t => new PropertySerializationModel(t)).ToList();

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
        public string Name { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public TypeSerializationModel BaseType { get; set; }

        [DataMember]
        public List<TypeSerializationModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }

        [DataMember]
        public AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public StaticEnum StaticEnum { get; set; }

        [DataMember]
        public SealedEnum SealedEnum { get; set; }

        [DataMember]
        public TypeKind Type { get; set; }

        [DataMember]
        public List<TypeSerializationModel> ImplementedInterfaces { get; set; }

        [DataMember]
        public List<TypeSerializationModel> NestedTypes { get; set; }

        [DataMember]
        public List<PropertySerializationModel> Properties { get; set; }

        [DataMember]
        public TypeSerializationModel DeclaringType { get; set; }

        [DataMember]
        public List<MethodSerializationModel> Methods { get; set; }

        [DataMember]
        public List<MethodSerializationModel> Constructors { get; set; }

        [DataMember]
        public List<FieldSerializationModel> Fields { get; set; }

        public static Dictionary<string, TypeSerializationModel> TypeDictionary = new Dictionary<string, TypeSerializationModel>();

    }
}

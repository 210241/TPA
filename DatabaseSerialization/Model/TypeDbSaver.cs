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
    public class TypeDbSaver
    {
        private TypeDbSaver()
        {

        }

        private TypeDbSaver(TypeBase baseType)
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

            Constructors = baseType.Constructors?.Select(t => new MethodDbSaver(t)).ToList();

            Fields = baseType.Fields?.Select(t => new FieldDbSaver(t)).ToList();

            GenericArguments = baseType.GenericArguments?.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces?.Select(GetOrAdd).ToList();

            Methods = baseType.Methods?.Select(t => new MethodDbSaver(t)).ToList();

            NestedTypes = baseType.NestedTypes?.Select(GetOrAdd).ToList();

            Properties = baseType.Properties?.Select(t => new PropertyDbSaver(t)).ToList();

        }

        public static TypeDbSaver GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (TypeDictionary.ContainsKey(baseType.Name))
                {
                    return TypeDictionary[baseType.Name];
                }
                else
                {
                    return new TypeDbSaver(baseType);
                }
            }
            else
                return null;
        }


        public string Name { get; set; }


        public string NamespaceName { get; set; }


        public TypeDbSaver BaseType { get; set; }


        public List<TypeDbSaver> GenericArguments { get; set; }


        public AccessLevel AccessLevel { get; set; }


        public AbstractEnum AbstractEnum { get; set; }


        public StaticEnum StaticEnum { get; set; }


        public SealedEnum SealedEnum { get; set; }


        public TypeKind Type { get; set; }


        public List<TypeDbSaver> ImplementedInterfaces { get; set; }


        public List<TypeDbSaver> NestedTypes { get; set; }


        public List<PropertyDbSaver> Properties { get; set; }


        public TypeDbSaver DeclaringType { get; set; }


        public List<MethodDbSaver> Methods { get; set; }


        public List<MethodDbSaver> Constructors { get; set; }


        public List<FieldDbSaver> Fields { get; set; }

        public static Dictionary<string, TypeDbSaver> TypeDictionary = new Dictionary<string, TypeDbSaver>();

    }
}

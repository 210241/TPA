using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;
using Base.Enums;


namespace SerializationXml.Model
{
    [DataContract(Name = "MethodReader")]
    public class MethodSerializationModel : MethodBase
    {
        private MethodSerializationModel()
        {
        }

        public MethodSerializationModel(MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeSerializationModel.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;
            foreach (var baseGenericArgument in baseMethod.GenericArguments)
            {
                this.GenericArguments.Add(TypeSerializationModel.GetOrAdd(baseGenericArgument));
            }

            foreach (var baseParameter in baseMethod.Parameters)
            {
                this.Parameters.Add(new ParameterSerializationModel(baseParameter));
            }
        }

        [DataMember]
        public new string Name { get; set; }

        [DataMember]
        public new List<TypeSerializationModel> GenericArguments { get; set; }

        [DataMember]
        public new AccessLevel AccessLevel { get; set; }

        [DataMember]
        public new AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public new StaticEnum StaticEnum { get; set; }

        [DataMember]
        public new VirtualEnum VirtualEnum { get; set; }

        [DataMember]
        public new TypeSerializationModel ReturnType { get; set; }

        [DataMember]
        public new bool Extension { get; set; }

        [DataMember]
        public new List<ParameterSerializationModel> Parameters { get; set; }

    }
}

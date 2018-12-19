using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;


namespace SerializationXml.Model
{
    [DataContract(Name = "ParameterSerializationModel", IsReference = true)]
    public class ParameterSerializationModel
    {
        private ParameterSerializationModel()
        {

        }

        public ParameterSerializationModel(ParameterBase baseParameter)
        {
            this.Name = baseParameter.Name;
            this.Type = TypeSerializationModel.GetOrAdd(baseParameter.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeSerializationModel Type { get; set; }

    }
}

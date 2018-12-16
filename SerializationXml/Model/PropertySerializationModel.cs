using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;

namespace SerializationXml.Model
{
    [DataContract(Name = "PropertySerializationModel", IsReference = true)]
    public class PropertySerializationModel : PropertyBase
    {
        private PropertySerializationModel()
        {

        }

        public PropertySerializationModel(PropertyBase baseProperty)
        {
            this.Name = baseProperty.Name;
            this.Type = TypeSerializationModel.GetOrAdd(baseProperty.Type);
        }

        [DataMember]
        public new string Name { get; set; }

        [DataMember]
        public new TypeSerializationModel Type { get; set; }
    }
}

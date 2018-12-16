using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;

namespace SerializationXml.Model
{
    [DataContract(Name = "NamespaceSerializationModel", IsReference = true)]
    public class NamespaceSerializationModel : NamespaceBase
    {
        private NamespaceSerializationModel()
        {

        }

        public NamespaceSerializationModel(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            Types = new List<TypeSerializationModel>();
            foreach (TypeBase baseElem in namespaceBase.Types)
            {
                Types.Add(TypeSerializationModel.GetOrAdd(baseElem));
            }
        }

        [DataMember]
        public new string Name { get; set; }

        [DataMember]
        public new List<TypeSerializationModel> Types { get; set; }
    }
}

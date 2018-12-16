using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;


namespace SerializationXml.Model
{
    [DataContract(Name = "AssemblySerializationModel", IsReference = true)]
    public class AssemblySerializationModel : AssemblyBase
    {
        private AssemblySerializationModel()
        {

        }

        public AssemblySerializationModel(AssemblyBase assemblyBase)
        {
            this.Name = assemblyBase.Name;
            Namespaces = new List<NamespaceSerializationModel>();
            foreach (var baseElem in assemblyBase.Namespaces)
            {
                Namespaces.Add(new NamespaceSerializationModel(baseElem));
            }

        }

        [DataMember]
        public new List<NamespaceSerializationModel> Namespaces { get; set; }

        [DataMember]
        public new string Name { get; set; }
    }
}

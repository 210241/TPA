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
    public class AssemblySerializationModel
    {
        private AssemblySerializationModel()
        {

        }

        public AssemblySerializationModel(AssemblyBase assemblyBase)
        {
            this.Name = assemblyBase.Name;
            Namespaces = assemblyBase.Namespaces?.Select(ns => new NamespaceSerializationModel(ns)).ToList();

        }

        [DataMember]
        public List<NamespaceSerializationModel> Namespaces { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}

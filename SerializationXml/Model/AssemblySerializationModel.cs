using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;


namespace SerializationXml.Model
{
    [DataContract(Name = "AssemblyModel")]
    public class AssemblySerializationModel : AssemblyBase
    {
        private AssemblySerializationModel()
        {

        }

        public AssemblySerializationModel(AssemblyBase assemblyBase)
        {
            this.Name = assemblyBase.Name;
            NamespaceSerializationModels = new List<NamespaceSerializationModel>();
            foreach (var baseElem in base.Namespaces)
            {
                NamespaceSerializationModels.Add(new NamespaceSerializationModel(baseElem));
            }

        }

        [DataMember]
        public  List<NamespaceSerializationModel> NamespaceSerializationModels { get; set; }

        [DataMember]
        public new string Name { get; set; }
    }
}

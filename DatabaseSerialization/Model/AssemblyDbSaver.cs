using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;


namespace SerializationXml.Model
{
    
    public class AssemblyDbSaver
    {
        private AssemblyDbSaver()
        {

        }

        public AssemblyDbSaver(AssemblyBase assemblyBase)
        {
            this.Name = assemblyBase.Name;
            Namespaces = assemblyBase.Namespaces?.Select(ns => new NamespaceDbSaver(ns)).ToList();

        }

        public List<NamespaceDbSaver> Namespaces { get; set; }

        public string Name { get; set; }
    }
}

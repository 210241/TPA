using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;

namespace SerializationXml.Model
{
    public class NamespaceDbSaver
    {
        private NamespaceDbSaver()
        {

        }

        public NamespaceDbSaver(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(t => TypeDbSaver.GetOrAdd(t)).ToList();
        }

        
        public string Name { get; set; }

        
        public List<TypeDbSaver> Types { get; set; }
    }
}

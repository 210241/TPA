using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;


namespace SerializationXml.Model
{
    public class ParameterDbSaver
    {
        private ParameterDbSaver()
        {

        }

        public ParameterDbSaver(ParameterBase baseParameter)
        {
            this.Name = baseParameter.Name;
            this.Type = TypeDbSaver.GetOrAdd(baseParameter.Type);
        }

        
        public string Name { get; set; }

        
        public TypeDbSaver Type { get; set; }

    }
}

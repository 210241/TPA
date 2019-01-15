using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;

namespace SerializationXml.Model
{
    public class PropertyDbSaver
    {
        private PropertyDbSaver()
        {

        }

        public PropertyDbSaver(PropertyBase baseProperty)
        {
            this.Name = baseProperty.Name;
            this.Type = TypeDbSaver.GetOrAdd(baseProperty.Type);
        }

        
        public string Name { get; set; }

        
        public TypeDbSaver Type { get; set; }
    }
}

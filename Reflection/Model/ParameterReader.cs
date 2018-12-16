using System.Runtime.Serialization;

namespace Reflection.Model
{
    [DataContract(Name = "ParameterReader")]
    public class ParameterReader
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeReader Type { get; set; }

        private ParameterReader()
        {

        }

        public ParameterReader(string name, TypeReader typeReader)
        {
            Name = name;
            Type = typeReader;
        }
    }
}

using System.Runtime.Serialization;
using Base.Model;

namespace Reflection.LogicModel
{

    public class ParameterLogicReader
    {
        public string Name { get; set; }

        public TypeLogicReader Type { get; set; }

        private ParameterLogicReader()
        {

        }

        public ParameterLogicReader(string name, TypeLogicReader typeReader)
        {
            Name = name;
            Type = typeReader;
        }

        public ParameterLogicReader(ParameterBase baseElement)
        {
            Name = baseElement.Name;
            Type = TypeLogicReader.GetOrAdd( baseElement.Type);
        }
    }
}
using System.Runtime.Serialization;
using Base.Model;
using Reflection.LogicModel;

namespace Reflection.Model
{
    public class ParameterReader : ParameterBase
    {

        public ParameterReader(ParameterBase baseElement)
        {
            Name = baseElement.Name;
            Type = baseElement.Type;
        }
        public ParameterReader(ParameterLogicReader baseElement)
        {
            Name = baseElement.Name;
            Type = TypeReader.GetOrAdd(baseElement.Type);
        }
    }
}

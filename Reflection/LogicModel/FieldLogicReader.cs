using System.Reflection;
using Base.Model;

namespace Reflection.LogicModel
{
    public class FieldLogicReader
    {
        public string Name { get; set; }

        public TypeLogicReader Type { get; set; }

        public FieldLogicReader(string name, TypeLogicReader typeLogicReader )
        {
            Name = name;
            Type = typeLogicReader;
        }
        
    }
}
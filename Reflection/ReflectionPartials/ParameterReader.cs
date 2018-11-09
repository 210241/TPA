namespace Reflection.ReflectionPartials
{
   
    public class ParameterReader
    {
        
        public string Name { get; set; }
        
        public TypeReader Type { get; set; }

        public ParameterReader(string name, TypeReader typeReader)
        {
            Name = name;
            Type = typeReader;
        }
    }
}

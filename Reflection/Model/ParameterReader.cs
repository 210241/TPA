namespace Reflection.Model
{
   
    public class ParameterReader
    {
        
        public string Name { get; set; }
        
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

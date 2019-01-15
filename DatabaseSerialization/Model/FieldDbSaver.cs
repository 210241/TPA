using Base.Model;

namespace SerializationXml.Model
{
    public class FieldDbSaver
    {
        private FieldDbSaver()
        {

        }

        public FieldDbSaver(FieldBase baseParameter)
        {
            this.Name = baseParameter.Name;
            this.Type = TypeDbSaver.GetOrAdd(baseParameter.Type);
        }

        
        public string Name { get; set; }

        
        public TypeDbSaver Type { get; set; }
    }
}
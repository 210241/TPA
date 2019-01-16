using System.Collections.Generic;
using Base.Model;

namespace DatabaseSerialization.Model
{
    public class FieldDbSaver
    {
        private FieldDbSaver()
        {
            TypeFields = new HashSet<TypeDbSaver>();
        }
        public int FieldDbSaverId{ get; set; }

        
        public FieldDbSaver(FieldBase baseParameter)
        {
            this.Name = baseParameter.Name;
            this.Type = TypeDbSaver.GetOrAdd(baseParameter.Type);
        }

        
        public string Name { get; set; }

        
        public TypeDbSaver Type { get; set; }
        
        
        public ICollection<TypeDbSaver> TypeFields { get; set; }
    }
}
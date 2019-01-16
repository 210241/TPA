using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Base.Enums;
using Base.Model;

namespace DatabaseSerialization.Model
{
    public class MethodDbSaver
    {
        private MethodDbSaver()
        {
            TypeConstructors = new HashSet<TypeDbSaver>();
            TypeMethods = new HashSet<TypeDbSaver>();
        }
        
        public int MethodDbSaverId{ get; set; }


        public MethodDbSaver(MethodBase baseMethod)
        {
            
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeDbSaver.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(TypeDbSaver.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterDbSaver(t)).ToList();
        }

        public string Name { get; set; }

        public List<TypeDbSaver> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        
        public AbstractEnum AbstractEnum { get; set; }

        
        public StaticEnum StaticEnum { get; set; }

        
        public VirtualEnum VirtualEnum { get; set; }

        
        public TypeDbSaver ReturnType { get; set; }

        
        public bool Extension { get; set; }

        
        public List<ParameterDbSaver> Parameters { get; set; }
        
        public ICollection<TypeDbSaver> TypeConstructors { get; set; }

        public ICollection<TypeDbSaver> TypeMethods { get; set; } 
    }
}

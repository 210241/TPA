using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;
using Base.Enums;


namespace SerializationXml.Model
{
    public class MethodDbSaver
    {
        private MethodDbSaver()
        {
        }

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

    }
}

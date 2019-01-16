using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Interfaces;
using Base.Model;
using Reflection.LogicModel;



namespace Reflection
{
    public class PersistanceManager
    {
        [Import(typeof(ISerializator), AllowDefault = false)]
        public ISerializator Serializator { get; set; }
        public void Serialize(AssemblyLogicReader assemblyLogicReader)
        {
            AssemblyBase assemblyBase = DataTransferGraphMapper.AssemblyBase(assemblyLogicReader);
            
            Serializator.Serialize(assemblyBase);


        }

        public AssemblyLogicReader Deserialize()
        {
            AssemblyBase deserializedAssemblyReader = Serializator.Deserialize();


            AssemblyLogicReader assemblyLogicReader = new AssemblyLogicReader(deserializedAssemblyReader);

            return assemblyLogicReader;
        }
    }
}

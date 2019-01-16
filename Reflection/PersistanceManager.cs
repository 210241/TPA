using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Interfaces;
using Base.Model;
using DatabaseSerialization;
using Reflection.LogicModel;



namespace Reflection
{
    public class PersistanceManager
    {
        [Import(typeof(ISerializator), AllowDefault = false)]
        public ISerializator Serializator { get; set; }
        public void SerializeToXml(AssemblyLogicReader assemblyLogicReader, string connectionString)
        {
            AssemblyBase assemblyBase = DataTransferGraphMapper.AssemblyBase(assemblyLogicReader);

            Serializator.Serialize(assemblyBase, connectionString);


        }

        public AssemblyLogicReader DeserializeFromXml(string connectionString)
        {
            AssemblyBase deserializedAssemblyReader = Serializator.Deserialize(connectionString);


            AssemblyLogicReader assemblyLogicReader = new AssemblyLogicReader(deserializedAssemblyReader);

            return assemblyLogicReader;
        }
    }
}

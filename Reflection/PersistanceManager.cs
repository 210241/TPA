using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Interfaces;
using Base.Model;
using Reflection.LogicModel;
using Reflection.Model;
using SerializationXml;


namespace Reflection
{
    public class PersistanceManager
    {

        public ISerializator Serializator = new XmlSerializator();
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

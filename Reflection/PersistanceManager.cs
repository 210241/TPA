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
            AssemblyReader assemblyReader = new AssemblyReader(assemblyLogicReader);

            Serializator.Serialize(assemblyReader, connectionString);


        }

        public AssemblyLogicReader DeserializeFromXml(string connectionString)
        {
            AssemblyBase deserializedAssemblyReader = Serializator.Deserialize(connectionString);

            AssemblyReader assemblyReader = new AssemblyReader(deserializedAssemblyReader);

            AssemblyLogicReader assemblyLogicReader = new AssemblyLogicReader(assemblyReader);

            return assemblyLogicReader;
        }
    }
}

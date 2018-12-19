using System.IO;
using System.Runtime.Serialization;
using Base.Interfaces;
using Base.Model;
using SerializationXml.Model;

namespace SerializationXml
{
    public class XmlSerializator : ISerializator
    {
        DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(AssemblySerializationModel));

        public void Serialize(AssemblyBase assembly, string path)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(assembly);

            FileStream writer = new FileStream(path, FileMode.Create);
            xmlSerializer.WriteObject(writer, assemblySerializationModel);

            writer.Close();
        }

        public AssemblyBase Deserialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);

            AssemblyBase assembly = DataTransferGraphMapper.AssemblyBase((AssemblySerializationModel)xmlSerializer.ReadObject(fs));

            fs.Close();

            return assembly;
        }


    }
}

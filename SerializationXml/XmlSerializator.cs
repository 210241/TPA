using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Base.Interfaces;
using Base.Model;
using SerializationXml.Model;

namespace SerializationXml
{
    public class XmlSerializator : ISerializator
    {
        DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(AssemblySerializationModel));
        //XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssemblySerializationModel));

        public void Serialize(AssemblyBase assembly, string path)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(assembly);

            FileStream writer = new FileStream(path, FileMode.Create);
            xmlSerializer.WriteObject(writer, assemblySerializationModel);
            //xmlSerializer.Serialize(writer, assemblySerializationModel);

            writer.Close();
        }

        public AssemblyBase Deserialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);

            AssemblyBase assembly = (AssemblyBase)xmlSerializer.ReadObject(fs);
            //AssemblyBase assembly = (AssemblyBase)xmlSerializer.Deserialize(fs);

            fs.Close();

            return assembly;
        }


    }
}

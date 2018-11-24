using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Reflection.Model;

namespace SerializationXml
{
    public class XmlSerializator
    {
        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssemblyReader));

        public void Serialize(AssemblyReader assembly, string path)
        {
            StreamWriter streamWriter = new StreamWriter(path);
            xmlSerializer.Serialize(streamWriter, assembly);
            streamWriter.Close();
        }

        public AssemblyReader Deserialize(string path)
        {
            AssemblyReader assembly;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            assembly = (AssemblyReader)xmlSerializer.Deserialize(fileStream);
            fileStream.Close();

            return assembly;
        }


    }
}

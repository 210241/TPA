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
using Reflection.Model;

namespace SerializationXml
{
    public class XmlSerializator
    {
        DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(AssemblyReader));

        public void Serialize(AssemblyReader assembly, string path)
        {
            FileStream writer = new FileStream(path, FileMode.Create);
            xmlSerializer.WriteObject(writer, assembly);
            writer.Close();
        }

        public AssemblyReader Deserialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            AssemblyReader assembly = (AssemblyReader)xmlSerializer.ReadObject(fs);
            fs.Close();

            return assembly;
        }


    }
}

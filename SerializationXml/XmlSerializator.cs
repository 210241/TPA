using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using Base.Exception;
using Base.Interfaces;
using Base.Model;
using SerializationXml.Model;

namespace SerializationXml
{
    [Export(typeof(ISerializator))]
    public class XmlSerializator : ISerializator
    {
        DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(AssemblySerializationModel));

        public void Serialize(AssemblyBase assembly)
        {

            try
            {
                string path = GetFilePath();
                AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(assembly);

                FileStream writer = new FileStream(path, FileMode.Create);
                xmlSerializer.WriteObject(writer, assemblySerializationModel);

                writer.Close();
            }
            catch (FilePathException e)
            {
                throw new SaveReadException(e.Message);
            }
            
            
        }
        
        public AssemblyBase Deserialize()
        {
            try
            {
                string path = GetFilePath();
                
                FileStream fs = new FileStream(path, FileMode.Open);

                AssemblyBase assembly = DataTransferGraphMapper.AssemblyBase((AssemblySerializationModel)xmlSerializer.ReadObject(fs));

                fs.Close();

                return assembly;
            }
            catch (FilePathException e)
            {
               
                throw new SaveReadException(e.Message);
            }
            
           
        }

        private string GetFilePath()
        {
            string filePath = ConfigurationManager.AppSettings["filePathToDataSource"];
            if (string.IsNullOrEmpty(filePath))
            {
                throw new FilePathException("Provided file path is empty or null");
            }
            if (!filePath.EndsWith(".xml", StringComparison.InvariantCulture))
            {
                throw new FilePathException(
                    $"Provided file path {filePath} is invalid (unknown extension). Provide valid .xml file path");
            }

            return filePath;
        }

    }

}

using System.Reflection;
using Reflection.Model;

namespace Reflection
{
    public class Reflector
    {
        public AssemblyReader AssemblyReader { get; private set; }
        public Reflector(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            TypeReader.TypeDictionary.Clear();
            AssemblyReader = new AssemblyReader(assembly);
        }

        public Reflector(AssemblyReader assemblyReader)
        {
            AssemblyReader = assemblyReader;
            TypeReader.TypeDictionary.Clear();
            //AssemblyReader.NamespaceReader.ForEach(ns => ns.Types.ForEach(t => TypeReader.TypeDictionary.Add(t.Name, t) ));
        }
    }
}

using System.Reflection;
using Reflection.LogicModel;


namespace Reflection
{
    public class Reflector
    {
        public AssemblyLogicReader AssemblyLogicReader { get; private set; }
        public Reflector(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            TypeLogicReader.TypeDictionary.Clear();
            AssemblyLogicReader = new AssemblyLogicReader(assembly);
        }

        public Reflector(AssemblyLogicReader assemblyReader)
        {
            AssemblyLogicReader = assemblyReader;
            TypeLogicReader.TypeDictionary.Clear();
            //AssemblyReader.NamespaceReader.ForEach(ns => ns.Types.ForEach(t => TypeReader.TypeDictionary.Add(t.Name, t) ));
        }
    }
}

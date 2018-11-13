using System.Reflection;
using Reflection.Model;

namespace Reflection
{
    public class Reflector
    {
        public AssemblyReader AssemblyReader { get; private set; }
        public Reflector(Assembly assembly)
        {
            AssemblyReader = new AssemblyReader(assembly);
        }
        public Reflector(AssemblyReader assemblyReader)
        {
            AssemblyReader = assemblyReader;
        }

        public Reflector(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            AssemblyReader = new AssemblyReader(assembly);
        }
    }
}

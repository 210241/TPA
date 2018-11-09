using System.Reflection;
using Reflection.ReflectionPartials;

namespace Reflection
{
    public class Reflection
    {
        public AssemblyReader AssemblyReader { get; private set; }
        public Reflection(Assembly assembly)
        {
            AssemblyReader = new AssemblyReader(assembly);
        }
        public Reflection(AssemblyReader assemblyReader)
        {
            AssemblyReader = assemblyReader;
        }

        public Reflection(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            AssemblyReader = new AssemblyReader(assembly);
        }
    }
}

using System.Reflection;
using DataTransfer.Interfaces;
using DataTransfer.Model;


namespace Reflection
{
    public partial class Reflection : IDataStorageProvider
    {
        private readonly ILogger _logger;

        public Reflection(ILogger logger)
        {
            _logger = logger;
        }

        public AssemblyDataStorage GetDataStorage(string assemblyFilePath)
        {
            if (string.IsNullOrEmpty(assemblyFilePath))
            {
                throw new System.ArgumentNullException("Cannot find assembly file with path: " + assemblyFilePath);
            }

            Assembly assembly = Assembly.LoadFrom(assemblyFilePath);
            _logger.Trace("Opening assembly: " + assembly.FullName);

            return LoadAssemblyData(assembly);
        }
    }
}

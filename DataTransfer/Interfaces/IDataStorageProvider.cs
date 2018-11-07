
using DataTransfer.Model;

namespace DataTransfer.Interfaces
{
    public interface IDataStorageProvider
    {
        AssemblyDataStorage GetDataStorage(string connectionString);
    }
}

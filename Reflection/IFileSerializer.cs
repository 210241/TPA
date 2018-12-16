using Reflection.Model;

namespace Reflection
{
    public interface IFileSerializer
    {
        void Serialize(AssemblyReader assembly, string path);

        AssemblyReader Deserialize(string path);
    }
}
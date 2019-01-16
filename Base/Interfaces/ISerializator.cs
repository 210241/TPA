using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Model;

namespace Base.Interfaces
{
    public interface ISerializator
    {
        void Serialize(AssemblyBase assemblyBase);
        AssemblyBase Deserialize(); 
    }
}

using System.Collections.Generic;

namespace DataTransfer.Model
{
    public class AssemblyData : BaseData
    {
        public IEnumerable<NamespaceData> Namespaces { get; set; }
    }
}
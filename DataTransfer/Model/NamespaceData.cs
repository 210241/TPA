using System.Collections.Generic;

namespace DataTransfer.Model
{
    public class NamespaceData : BaseData
    {
        public IEnumerable<TypeData> Types { get; set; }
    }
}
using System.Collections.ObjectModel;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class AssemblyNodeItem : NodeItem
    {
        private readonly AssemblyReader _assemblyReader;

        public AssemblyNodeItem(AssemblyReader assembly)
            : base(assembly.Name, ItemTypeEnum.Assembly)
        {
            _assemblyReader = assembly;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_assemblyReader?.NamespaceReader != null)
            {
                foreach (NamespaceReader namespaceReader in _assemblyReader.NamespaceReader)
                {
                    children.Add(new NamespaceNodeItem(namespaceReader));
                }
            }
        }
    }
}

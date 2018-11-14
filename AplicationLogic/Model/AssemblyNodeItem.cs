using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class AssemblyNodeItem : NodeItem
    {
        private readonly AssemblyReader _assemblyReader;
        private readonly ILogger _logger;

        public AssemblyNodeItem(AssemblyReader assembly, ILogger logger)
            : base(assembly.Name, ItemTypeEnum.Assembly)
        {
            _assemblyReader = assembly;
            _logger = logger;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_assemblyReader?.NamespaceReader != null)
            {
                foreach (NamespaceReader namespaceReader in _assemblyReader.NamespaceReader)
                {
                    _logger.Trace($"Adding namespace: {namespaceReader.Name}");
                    children.Add(new NamespaceNodeItem(namespaceReader, _logger));
                }
            }
        }
    }
}

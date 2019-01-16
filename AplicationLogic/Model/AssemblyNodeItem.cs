using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class AssemblyNodeItem : NodeItem
    {
        private readonly AssemblyLogicReader _assemblyLogicReader;
        private readonly ILogger _logger;

        public AssemblyNodeItem(AssemblyLogicReader logicAssembly, ILogger logger)
            : base(logicAssembly.Name, ItemTypeEnum.Assembly)
        {
            _assemblyLogicReader = logicAssembly;
            _logger = logger;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_assemblyLogicReader?.NamespaceLogicReader != null)
            {
                foreach (NamespaceLogicReader namespaceLogicReader in _assemblyLogicReader.NamespaceLogicReader)
                {
                    _logger.Trace($"Adding namespace: {namespaceLogicReader.Name}");
                    children.Add(new NamespaceNodeItem(namespaceLogicReader, _logger));
                }
            }
        }
    }
}

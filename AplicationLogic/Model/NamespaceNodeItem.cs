using System.Collections.ObjectModel;
using DataTransfer.Model.Enums;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class NamespaceNodeItem : NodeItem
    {
        private readonly NamespaceReader _namespaceReader;

        public NamespaceNodeItem(NamespaceReader namespaceReader)
            : base(namespaceReader.Name, ItemTypeEnum.Namespace)
        {
            _namespaceReader = namespaceReader;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_namespaceReader?.Types != null)
            {
                foreach (TypeReader typeModel in _namespaceReader?.Types)
                {
                    ItemTypeEnum typeEnum = typeModel.Type == TypeKind.ClassType ?
                        ItemTypeEnum.Class : typeModel.Type == TypeKind.EnumType ?
                            ItemTypeEnum.Enum : typeModel.Type == TypeKind.InterfaceType ?
                                ItemTypeEnum.Interface : ItemTypeEnum.Struct;

                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeModel.Name], typeEnum));
                }
            }
        }
    }
}

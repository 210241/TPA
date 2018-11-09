using System.Collections.ObjectModel;
using DataTransfer.Model.Enums;
using Reflection.ReflectionPartials;

namespace ApplicationLogic.Model
{
    public class MethodNodeItem : NodeItem
    {
        public MethodReader MethodReader { get; set; }

        public MethodNodeItem(MethodReader methodReader, ItemTypeEnum type)
            : base(GetModifiers(methodReader) + methodReader.Name, type)
        {
            MethodReader = methodReader;
        }

        public static string GetModifiers(MethodReader methodReader)
        {
            string type = null;
            type += methodReader.Modifiers.Item1.ToString().ToLower() + " ";
            type += methodReader.Modifiers.Item2 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
            type += methodReader.Modifiers.Item3 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
            type += methodReader.Modifiers.Item4 == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : string.Empty;
            return type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (MethodReader.GenericArguments != null)
            {
                foreach (TypeReader genericArgument in MethodReader.GenericArguments)
                {
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[genericArgument.Name], ItemTypeEnum.GenericArgument));
                }
            }

            if (MethodReader.Parameters != null)
            {
                foreach (ParameterReader parameter in MethodReader.Parameters)
                {
                    children.Add(new ParameterNodeItem(parameter, ItemTypeEnum.Parameter));
                }
            }

            if (MethodReader.ReturnType != null)
            {
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[MethodReader.ReturnType.Name], ItemTypeEnum.ReturnType));
            }
        }
    }
}

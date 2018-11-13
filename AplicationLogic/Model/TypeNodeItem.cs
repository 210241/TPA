using System.Collections.ObjectModel;
using DataTransfer.Model.Enums;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class TypeNodeItem : NodeItem
    {
        private readonly TypeReader _typeReader;

        public TypeNodeItem(TypeReader typeReader, ItemTypeEnum type)
            : base(GetModifiers(typeReader) + typeReader.Name, type)
        {
            _typeReader = typeReader;
        }

        public static string GetModifiers(TypeReader model)
        {
            if (model.Modifiers != null)
            {
                string type = null;
                type += model.Modifiers.Item1.ToString().ToLower() + " ";
                type += model.Modifiers.Item2 == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : string.Empty;
                type += model.Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
                type += model.Modifiers.Item4 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
                return type;
            }

            return null;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_typeReader.BaseType != null)
            {
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[_typeReader.BaseType.Name], ItemTypeEnum.BaseType));
            }

            if (_typeReader.DeclaringType != null)
            {
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[_typeReader.DeclaringType.Name], ItemTypeEnum.Type));
            }

            if (_typeReader.Properties != null)
            {
                foreach (PropertyReader propertyReader in _typeReader.Properties)
                {
                    children.Add(new PropertyNodeItem(propertyReader, GetModifiers(propertyReader.Type) + propertyReader.Type.Name + " " + propertyReader.Name));
                }
            }

            if (_typeReader.Fields != null)
            {
                foreach (ParameterReader parameterModel in _typeReader.Fields)
                {
                    children.Add(new ParameterNodeItem(parameterModel, ItemTypeEnum.Field));
                }
            }

            if (_typeReader.GenericArguments != null)
            {
                foreach (TypeReader typeReader in _typeReader.GenericArguments)
                {
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], ItemTypeEnum.GenericArgument));
                }
            }

            if (_typeReader.ImplementedInterfaces != null)
            {
                foreach (TypeReader typeReader in _typeReader.ImplementedInterfaces)
                {
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], ItemTypeEnum.ImplementedInterface));
                }
            }

            if (_typeReader.NestedTypes != null)
            {
                foreach (TypeReader typeReader in _typeReader.NestedTypes)
                {
                    ItemTypeEnum type = typeReader.Type == TypeKind.ClassType ? ItemTypeEnum.NestedClass :
                        typeReader.Type == TypeKind.StructType ? ItemTypeEnum.NestedStructure :
                        typeReader.Type == TypeKind.EnumType ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], type));
                }
            }

            if (_typeReader.Methods != null)
            {
                foreach (MethodReader methodReader in _typeReader.Methods)
                {
                    children.Add(new MethodNodeItem(methodReader, methodReader.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method));
                }
            }

            if (_typeReader.Constructors != null)
            {
                foreach (MethodReader methodReader in _typeReader.Constructors)
                {
                    children.Add(new MethodNodeItem(methodReader, ItemTypeEnum.Constructor));
                }
            }
        }
    }
}

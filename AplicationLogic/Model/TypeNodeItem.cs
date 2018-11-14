using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Enums;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class TypeNodeItem : NodeItem
    {
        private readonly TypeReader _typeReader;
        private readonly ILogger _logger;

        public TypeNodeItem(TypeReader typeReader, ItemTypeEnum type, ILogger logger)
            : base(GetModifiers(typeReader) + typeReader.Name, type)
        {
            _typeReader = typeReader;
            _logger = logger;
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
                _logger.Trace($"Adding BaseType: [{ItemTypeEnum.BaseType.ToString()}] {_typeReader.BaseType.Name} implemented in Type: {_typeReader.Name}");
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[_typeReader.BaseType.Name], ItemTypeEnum.BaseType, _logger));
            }

            if (_typeReader.DeclaringType != null)
            {
                _logger.Trace($"Adding DeclaringType: [{ItemTypeEnum.Type.ToString()}] {_typeReader.DeclaringType.Name} implemented in Type: {_typeReader.Name}");
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[_typeReader.DeclaringType.Name], ItemTypeEnum.Type, _logger));
            }

            if (_typeReader.Properties != null)
            {
                foreach (PropertyReader propertyReader in _typeReader.Properties)
                {
                    _logger.Trace($"Adding Property: [{GetModifiers(propertyReader.Type) + propertyReader.Type.Name}] {propertyReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new PropertyNodeItem(propertyReader, GetModifiers(propertyReader.Type) + propertyReader.Type.Name + " " + propertyReader.Name, _logger));
                }
            }

            if (_typeReader.Fields != null)
            {
                foreach (ParameterReader parameterReader in _typeReader.Fields)
                {
                    _logger.Trace($"Adding Parameter: [{ItemTypeEnum.Field.ToString()}] {parameterReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new ParameterNodeItem(parameterReader, ItemTypeEnum.Field, _logger));
                }
            }

            if (_typeReader.GenericArguments != null)
            {
                foreach (TypeReader typeReader in _typeReader.GenericArguments)
                {
                    _logger.Trace($"Adding Type: [{ItemTypeEnum.GenericArgument.ToString()}] {typeReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], ItemTypeEnum.GenericArgument, _logger));
                }
            }

            if (_typeReader.ImplementedInterfaces != null)
            {
                foreach (TypeReader typeReader in _typeReader.ImplementedInterfaces)
                {
                    _logger.Trace($"Adding Type: [{ItemTypeEnum.ImplementedInterface.ToString()}] {typeReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], ItemTypeEnum.ImplementedInterface, _logger));
                }
            }

            if (_typeReader.NestedTypes != null)
            {
                foreach (TypeReader typeReader in _typeReader.NestedTypes)
                {
                    ItemTypeEnum type = typeReader.Type == TypeKind.ClassType ? ItemTypeEnum.NestedClass :
                        typeReader.Type == TypeKind.StructType ? ItemTypeEnum.NestedStructure :
                        typeReader.Type == TypeKind.EnumType ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;

                    _logger.Trace($"Adding Type: [{type.ToString()}] {typeReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], type, _logger));
                }
            }

            if (_typeReader.Methods != null)
            {
                foreach (MethodReader methodReader in _typeReader.Methods)
                {
                    ItemTypeEnum type = methodReader.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method;
                    _logger.Trace($"Adding Method: [{type.ToString()}] {methodReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new MethodNodeItem(methodReader, type, _logger));
                }
            }

            if (_typeReader.Constructors != null)
            {
                foreach (MethodReader methodReader in _typeReader.Constructors)
                {
                    _logger.Trace($"Adding Method: [{ItemTypeEnum.Constructor.ToString()}] {methodReader.Name} implemented in Type: {_typeReader.Name}");
                    children.Add(new MethodNodeItem(methodReader, ItemTypeEnum.Constructor, _logger));
                }
            }
        }
    }
}

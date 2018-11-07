namespace DataTransfer.Model
{
    public class ParameterData : BaseData
    {
        public TypeData TypeMetadata { get; set; }

        public ParameterData(string name, TypeData typeMetadataDto)
        {
            Name = name;
            TypeMetadata = typeMetadataDto;
        }
    }
}
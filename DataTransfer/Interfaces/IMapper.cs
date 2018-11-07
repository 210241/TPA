namespace DataTransfer.Interfaces
{
    public interface IMapper<Source, Target>
    {
        Target Map(Source objectToMap);
    }
}

namespace Base.Exception
{
    public class SaveReadException : System.Exception
    {
        public SaveReadException()
        {
        }

        public SaveReadException(string message) : base(message)
        {
        }

        public SaveReadException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
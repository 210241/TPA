namespace Base.Exception
{
    public class FilePathException : System.Exception
    {
        public FilePathException()
        {
        }

        public FilePathException(string message) : base(message)
        {
        }

        public FilePathException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
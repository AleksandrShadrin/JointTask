namespace IOUtilities.Exceptions
{
    public class FileNotExistException : FileException
    {
        public FileNotExistException(string filePath) : base($"file with path: {filePath} not exist")
        { }
    }
}

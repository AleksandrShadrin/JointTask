namespace IOUtilities.Exceptions
{
    public class FileExistException : FileException
    {
        public FileExistException(string filePath) : base($"file with path: {filePath} already exist")
        { }
    }
}

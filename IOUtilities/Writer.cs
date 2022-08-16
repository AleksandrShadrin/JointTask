using Core;
using Interfaces;
using IOUtilities.Exceptions;

namespace IOUtilities
{
    public class Writer : IWriter
    {

        public FileInfo File { get; set; }
     
        public Writer(string path)
        {
            File = new FileInfo(path);
        }
        /// <summary>
        ///Производит запись юзеров в файл
        /// </summary>
        /// <param name="userList"></param>
        /// <exception cref="FileExistException"></exception>
        public void Write(IEnumerable<User> userList)
        {
            if (File.Exists)
            {
                throw new FileExistException(File.FullName);
            }
            try
            {
                using (var sw = File.CreateText())
                {
                    foreach (var user in userList)
                    {
                        sw.WriteLine($"{user.GetAmount()}\t{user.GetFio()}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

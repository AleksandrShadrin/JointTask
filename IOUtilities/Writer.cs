using Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOUtilities
{
    public class Writer : IWriter
    {

        public FileInfo File { get; set; }
        public Writer(string path)
        {
            File = new FileInfo(path);
        }
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
                        sw.WriteLine($"{user.GetFio()}\t{user.GetAmount()}");
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

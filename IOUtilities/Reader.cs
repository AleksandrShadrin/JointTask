using Core;
using Interfaces;
using IOUtilities.Exceptions;

namespace IOUtilities
{
    public class Reader : IReader
    {
        private IParser _parser;
        private IValidator _validator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns></returns>
        /// <exception cref="FileNotExistException"></exception>
        public IEnumerable<User> Read(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            List<string> readText = new List<string>();

            if (!fileInfo.Exists)
            {
                throw new FileNotExistException(fileInfo.FullName);
            }

            // Построчное считывание файла
            try
            {
                using (StreamReader sr = fileInfo.OpenText())
                {

                    while (!sr.EndOfStream)
                    {
                        string? s = sr.ReadLine();
                        if(s != null)
                            readText.Add(s);
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

            foreach (string s in readText)
            {
                if (_validator.IsValid(s))
                {
                    yield return _parser.Parse(s);
                }
            }
        }

        public Reader(IParser parser, IValidator validator)
        {
            _parser = parser;
            _validator = validator;
        }
    }
}

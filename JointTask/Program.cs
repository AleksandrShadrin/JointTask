using Core;
using FinanceUtilities;
using InputProcessing;
using IOUtilities;
using Interfaces;
using IOUtilities.Exceptions;
using Log;

namespace JointTask
{
    
    public class Program
    {   
        public static void Main(string[] args)
        {

            Action<string> ExceptionLogger;
            Logger logger = new ConsoleLogger();
            ExceptionLogger = logger.Log;


            //читаем с файла
            IReader reader = new Reader(new Parser(), new Validator(ExceptionLogger));
            IEnumerable<User> users = reader.Read(@"C:\Users\User\Documents\mveuC#\1660602157406.txt");
            
            //обрабатываем
            IUsersRewards usersRewards = new UsersRewards();
            users = usersRewards.AddRewards(users, 20);

            try
            {
                long fileName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                //запись в файл
                IWriter writer = new Writer($@"C:\Users\User\Documents\mveuC#\{fileName}.txt");
                writer.Write(users);
                //можно добавить из какого файла взяли
                Console.WriteLine($"Обработка произошла успешно, данные выведены в файл {fileName}.txt");
            }
            catch(FileException ex)
            {
                ExceptionLogger?.Invoke(ex.Message);
                //Console.WriteLine(ex.Message);
            }  
        }

    }
}
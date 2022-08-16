using Core;
using FinanceUtilities;
using InputProcessing;
using IOUtilities;
using Interfaces;
using IOUtilities.Exceptions;

namespace JointTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //читаем с файла
            IReader reader = new Reader(new Parser(), new Validator());
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
                Console.WriteLine(ex.Message);
            }  
        }

    }
}
using Core;
using FinanceUtilities;
using InputProcessing;
using IOUtilities;
using Interfaces;
using IOUtilities.Exceptions;
using Log;
using Loader;


//убрать логгер из валидатора
namespace JointTask
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.ReadLine();
            //Logs
            Action<string> ExceptionLogger;
            Logger logger = new ConsoleLogger();
            ExceptionLogger = logger.Log;

            //LoadConfigure
            ConfigLoader configLoader = new ConfigLoader();
            configLoader.LoadConfig();
            //читаем с файла
            IReader reader = new Reader(new Parser(), new Validator(ExceptionLogger));
            //IEnumerable<User> users = reader.Read(@"C:\Users\User\Documents\mveuC#\1660602157406.txt");
            if (String.IsNullOrWhiteSpace(configLoader.TryGetValue("sourceFile")) == false)
            {
                IEnumerable<User> users = reader.Read(configLoader.TryGetValue("sourceFile"));
                //обрабатываем
                IUsersRewards usersRewards = new UsersRewards();
                //users = usersRewards.AddRewards(users, 20);
                if (String.IsNullOrWhiteSpace(configLoader.TryGetValue("reward")) == false)
                {
                    users = usersRewards.AddRewards(users, int.Parse(configLoader.TryGetValue("reward")));
                }
                try
                {
                    long fileName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    //запись в файл
                    //IWriter writer = new Writer($@"C:\Users\User\Documents\mveuC#\{fileName}.txt");
                    IWriter writer = new Writer(configLoader.TryGetValue("reportFile"));
                    writer.Write(users);
                    //можно добавить из какого файла взяли
                    Console.WriteLine($"Обработка произошла успешно, данные выведены в файл {fileName}.txt");
                }
                catch (FileException ex)
                {
                    ExceptionLogger?.Invoke(ex.Message);
                    //Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
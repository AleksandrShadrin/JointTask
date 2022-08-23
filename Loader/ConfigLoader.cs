namespace Loader
{
    public class ConfigLoader
    {
        public string SourceFile { get; private set; }
        public string ReportFile { get; private set; }
        private int reward;
        private string[] inputData;

        
        public int Reward
        {
            get
            {
                return reward;
            }
            set
            {
                if (value >= 0)
                    reward = value;
                else
                    throw new ArgumentException();
            }
        }


        public ConfigLoader()
        {
            this.SourceFile = "";
            this.ReportFile = $@"C:\Users\User\Documents\mveuC#\" + 
                $@"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()}.txt";
                
            this.reward = 0;
        }

        public void LoadConfig()
        {
            SearchingSystemDirectory();
            SearchingHomeDirectory();
            GetFromParams();   
        }


        private void SearchingSystemDirectory()
        {
            string[] allFoundFiles = Directory.GetFiles(@"C:\ProgramData", "uniqueConfig.txt");
            try
            {
                inputData = File.ReadAllLines(allFoundFiles[0] + @"\uniqueConfig.txt");
                AssignValues(inputData);
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private void SearchingHomeDirectory()
        {
            string[] allFoundFiles = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Local", "uniqueConfig.txt");
            try
            {
                inputData = File.ReadAllLines(allFoundFiles[0] + @"\uniqueConfig.txt");
                AssignValues(inputData);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void GetFromParams()
        {
            this.AssignValues(string.Join(" ", Environment.GetCommandLineArgs()).Split("-"));
        }

        private void AssignValues(string[] args)
        {
            foreach (string arg in args)
            {
                string[] values = arg.Split(" ");

                if (!string.IsNullOrEmpty(values[1]))
                    switch (values[0])
                    {
                        case "sourceFile":
                            SourceFile = values[1];
                            break;
                        case "reportFile":
                            ReportFile = values[1];
                            break;
                        case "reward":
                            reward = int.Parse(values[1]);
                            break;
                    }
            }
        }



    }
}
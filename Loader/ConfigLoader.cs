using System.Collections;

namespace Loader
{
    public class ConfigLoader : ICloneable, IComparable<ConfigLoader>, IEnumerator<KeyValuePair<string, string>>
    {
        public KeyValuePair<string, string> Current => _config.ElementAtOrDefault(currentPosition);

        private Dictionary<string, string> _config = new();
        private int currentPosition = 0;
        object IEnumerator.Current => _config.ElementAtOrDefault(currentPosition);

        public void LoadConfig()
        {
            SearchInDirectory(Environment.SpecialFolder.ApplicationData);
            SearchInDirectory(Environment.SpecialFolder.LocalApplicationData);
            GetFromParams();
        }

        private ConfigLoader(Dictionary<string, string> config)
        {
            _config = config;
        }

        private void SearchInDirectory(Environment.SpecialFolder specialFolder)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(specialFolder), "uniqueConfig.txt");
            try
            {
                StreamReader sr;
                using (sr = File.OpenText(filePath))
                {
                    string str;
                    while (String.IsNullOrWhiteSpace(str = sr.ReadLine()) == false)
                    {
                        TryAssignValue(str);
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private void GetFromParams()
        {
            var values = string.Join(" ", Environment.GetCommandLineArgs()).Split("-");
            if (values[0] != null)
            {
                TryAssignValue("sourceFile" + values[0]);
            }
            if (values[1] != null)
            {
                TryAssignValue("reportFile" + values[1]);
            }
            if (values[2] != null)
            {
                TryAssignValue("reward" + values[2]);
            }
        }

        private void TryAssignValue(string value)
        {
            string[] values = value.Split(" ");
            if (!string.IsNullOrWhiteSpace(values[1]))
                switch (values[0])
                {
                    case "sourceFile":
                        TryAddValueToDictionary(values);
                        break;
                    case "reportFile":
                        TryAddValueToDictionary(values);
                        break;
                    case "reward":
                        TryAddValueToDictionary(values);
                        break;
                }
        }
        private void TryAddValueToDictionary(string[] values)
        {
            if (_config.ContainsKey(values[0]))
            {
                _config[values[0]] = values[1];
            }
            _config.Add(values[0], values[1]);
        }
        public int CompareTo(ConfigLoader? other)
        {
            if (other is not null)
            {
                return this._config.Count - other._config.Count;
            }
            return 1;
        }
        public object Clone()
        {
            return new ConfigLoader(this._config);
        }
        public bool MoveNext()
        {
            if (_config.Count >= currentPosition)
            {
                currentPosition += 1;
                return true;
            }
            return false;
        }
        public void Reset()
        {
            currentPosition = 0;
        }
        public void Dispose()
        {
            _config.Clear();
        }
    }
}
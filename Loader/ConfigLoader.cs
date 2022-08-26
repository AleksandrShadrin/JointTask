using System.Collections;

namespace Loader
{
    public class ConfigLoader : ICloneable, IComparable<ConfigLoader>, IEnumerator<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>
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
        public ConfigLoader()
        {

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
            var values = string.Join(" ", Environment.GetCommandLineArgs().Skip(1)).Split("-");

            if (String.IsNullOrWhiteSpace(values.ElementAtOrDefault(01)) == false)
            {
                TryAssignValue("sourceFile " + values[0]);
            }
            if (String.IsNullOrWhiteSpace(values.ElementAtOrDefault(1)) == false)
            {
                TryAssignValue("reportFile " + values[1]);
            }
            if (String.IsNullOrWhiteSpace(values.ElementAtOrDefault(2)) == false)
            {
                TryAssignValue("reward " + values[2]);
            }
        }
        public string TryGetValue(string key)
        {
            if (_config.TryGetValue(key, out var value))
            {
                return value;
            }
            return "";
        }
        private void TryAssignValue(string value)
        {
            string[] values = value.Split(" ");
            if (!string.IsNullOrWhiteSpace(value) && values.Length == 2)
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
            var newDict = new Dictionary<string, string>();
            foreach (var value in this)
            {
                newDict.TryAdd(value.Key, value.Value);
            }
            return new ConfigLoader(newDict);
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

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
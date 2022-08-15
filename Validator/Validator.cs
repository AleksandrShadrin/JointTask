using Interfaces;

namespace InputProcessing
{
    public class Validator : IValidator
    {
        public bool IsValid(string line)
        {
            var data = line.Split('\t');
            if (data.Length != 2) 
                return false;
            return int.TryParse(data[1], out _);
        }
    }
}
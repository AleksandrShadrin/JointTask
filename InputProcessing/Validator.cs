using Interfaces;

namespace InputProcessing
{
    public class Validator : IValidator
    {
        private Action<string> ExceptionLogger;
        public bool IsValid(string line)
        {
            var data = line.Split('\t');
            if (data.Length != 2)
            {
                ExceptionLogger?.Invoke("НеКоРрЕкТнЫй ВвОд СтРоКи: " + line);
                return false;
            }
                
            return int.TryParse(data[1], out _);
        }

        public Validator(Action<string> ExceptionLogger)
        {
            this.ExceptionLogger = ExceptionLogger;
        }
        public Validator()
        {
            
        }
    }
}
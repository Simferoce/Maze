namespace Game.Core
{
    public interface ILogger
    {
        public enum LogLevel
        {
            Debug,
            Information,
            Warning,
            Error,
        }

        public void Log(string message, LogLevel logLevel);
    }
}

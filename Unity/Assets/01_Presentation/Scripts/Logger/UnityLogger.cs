using UnityEngine;

namespace Game.Presentation
{
    public class UnityLogger : Game.Core.ILogger
    {
        public void Log(string message, Game.Core.ILogger.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Game.Core.ILogger.LogLevel.Debug:
                    Debug.Log(message);
                    break;
                case Core.ILogger.LogLevel.Information:
                    Debug.Log(message);
                    break;
                case Core.ILogger.LogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case Core.ILogger.LogLevel.Error:
                    Debug.LogError(message);
                    break;
            }
        }
    }
}

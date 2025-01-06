using System.Collections.Generic;

namespace Firebase.Analytics
{
    public interface IAnalyticsService
    {
        public void LogEvent(string eventName, Dictionary<string, int> parameters);
        public void LogEventStartGame();
    }
}


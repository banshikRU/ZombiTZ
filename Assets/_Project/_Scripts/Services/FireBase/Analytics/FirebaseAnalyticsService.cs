using System.Collections.Generic;

namespace Firebase.Analytics
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        public void LogEventStartGame()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
        }

        public void LogEvent(string eventName, Dictionary<string, int> parameters)
        {
            var firebaseParams = new Parameter[parameters.Count];
            int index = 0;

            foreach (var param in parameters)
            {
                firebaseParams[index++] = new Parameter(param.Key, param.Value.ToString());
            }

            FirebaseAnalytics.LogEvent(eventName, firebaseParams);
        }

    }
}


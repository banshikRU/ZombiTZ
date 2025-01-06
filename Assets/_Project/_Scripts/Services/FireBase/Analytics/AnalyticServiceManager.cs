using System.Collections.Generic;

namespace Firebase.Analytics
{
    public class AnalyticServiceManager
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly AnalyticsDataCollector _dataCollector;

        public AnalyticServiceManager(AnalyticsDataCollector dataCollector)
        {
            _analyticsService = new FirebaseAnalyticsService();
            _dataCollector = dataCollector;
        }

        public void LogEventStartGame()
        {
            _analyticsService.LogEventStartGame();
        }

        public void LogEvent(string eventName, Dictionary<string, int> parameters)
        {
            _analyticsService.LogEvent(eventName, parameters);
        }

        public void LogEventEndGame()
        {
            foreach (var parameter in _dataCollector.AnalizedParameters)
            {
               // Debug.Log($"key: {parameter.Key}  value: {parameter.Value}");
            }
            LogEvent("End Game", _dataCollector.AnalizedParameters);
        }
    }
}



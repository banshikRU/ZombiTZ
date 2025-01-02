using Firebase.Analytics;
using UnityEngine;

public class FirebaseAnalyticsService : IAnalyticsService
{
    public void LogEvent(string eventName, string parameter)
    {
       
    }

    public void LogEventStartGame()
    {
        Debug.Log("Start Game event");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
    }
}

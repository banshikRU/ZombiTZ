public interface IAnalyticsService 
{
    void LogEvent(string eventName, string parameter);
    void LogEventStartGame();
}

public class AnalyticServiceManager 
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticServiceManager()
    {
        _analyticsService = new FirebaseAnalyticsService();
    }

    public void LogEvent()
    {
        _analyticsService.LogEventStartGame();
    }
}

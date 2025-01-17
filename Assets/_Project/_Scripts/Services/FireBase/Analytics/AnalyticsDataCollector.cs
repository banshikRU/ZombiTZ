using System.Collections.Generic;

namespace Firebase.Analytics
{
    public class AnalyticsDataCollector
    {
        public Dictionary<string, int> AnalizedParameters { get; private set; }

        AnalyticsDataCollector()
        {
            AnalizedParameters = new Dictionary<string, int>();
        }

        public void AddAnalyzedParameterValue(string parameterName, int parameterValue)
        {
            if (AnalizedParameters.ContainsKey(parameterName))
            {
                AnalizedParameters[parameterName] += parameterValue;
            }
            else
            {
                AnalizedParameters.Add(parameterName, parameterValue);
            }
        }
    }
}

    
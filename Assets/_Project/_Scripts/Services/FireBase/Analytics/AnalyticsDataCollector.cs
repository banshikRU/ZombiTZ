using System.Collections.Generic;

namespace Firebase.Analytics
{
    public class AnalyticsDataCollector
    {
        public Dictionary<string, int> AnalyzedParameters { get; }

        private AnalyticsDataCollector()
        {
            AnalyzedParameters = new Dictionary<string, int>();
        }

        public void AddAnalyzedParameterValue(string parameterName, int parameterValue)
        {
            if (!AnalyzedParameters.TryAdd(parameterName, parameterValue))
            {
                AnalyzedParameters[parameterName] += parameterValue;
            }
        }
    }
}

    
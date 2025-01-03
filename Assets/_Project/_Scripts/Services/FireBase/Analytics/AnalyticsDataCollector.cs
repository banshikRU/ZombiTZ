using System.Collections.Generic;

namespace Firebase
{
    public class AnalyticsDataCollector
    {
        public Dictionary<string, int> AnalizedParameters { get; private set; }

        AnalyticsDataCollector()
        {
            AnalizedParameters = new Dictionary<string, int>();
        }

        public void AddAnalizedParameterValue(string parameterName, int parameterValue)
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

    
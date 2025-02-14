using System.Collections.Generic;
using Zenject;

namespace Firebase.Analytics
{
    public class AnalyticsDataCollector: IInitializable
    {
        public Dictionary<string, int> AnalyzedParameters { get; private set; }

        public void Initialize()
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

    
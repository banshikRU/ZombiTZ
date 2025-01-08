using UnityEngine;
using UnityEngine.Advertisements;

namespace Advertisements
{
    public class AdsInitializer : IUnityAdsInitializationListener
    {
        private readonly string _androidGameId;
        private readonly bool _testMode = true;
        private string _gameId;

        public AdsInitializer(string androidGameId)
        {
            _androidGameId = androidGameId;
            InitializeAds();
        }

        private void InitializeAds()
        {
#if UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId;
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }

        public void OnInitializationComplete()
        {
            //Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}

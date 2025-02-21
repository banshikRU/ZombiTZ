using System;
using _Project._Scripts.MainSceneBehaviour.SceneController;
using GameStateControl;
using Zenject;

namespace GameSystem
{
    public class MainSceneLoader : IInitializable, IDisposable
    {
        private readonly ServiceInitializer _serviceInitializer;
        private readonly SceneController _sceneController;

        public MainSceneLoader(ServiceInitializer serviceInitializer, SceneController sceneController)
        {
            _serviceInitializer = serviceInitializer;
            _sceneController = sceneController;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _serviceInitializer.OnInitializationCompleted += LoadMainScene;
        }

        public void Dispose()
        {
            _serviceInitializer.OnInitializationCompleted -= LoadMainScene;
        }
        
        private void LoadMainScene()
        {
            _sceneController.LoadScene(ScenesNames.MainScene);
        }
    }
}
using System;
using Services;
using Zenject;

namespace GameSystem
{
    public class BootstrapEntryPoint : IInitializable, IDisposable
    {
        private readonly ServiceInitializer _serviceInitializer;
        private readonly SceneController _sceneController;

        public BootstrapEntryPoint(ServiceInitializer serviceInitializer, SceneController sceneController)
        {
            _serviceInitializer = serviceInitializer;
            _sceneController = sceneController;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        public void Dispose()
        {
           UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            _serviceInitializer.OnInitializationCompleted -= LoadMainScene;
        }
        
        private void SubscribeEvents()
        {
            _serviceInitializer.OnInitializationCompleted += LoadMainScene;
        }
        
        private void LoadMainScene()
        {
            _sceneController.LoadGameScene();
        }
    }
}
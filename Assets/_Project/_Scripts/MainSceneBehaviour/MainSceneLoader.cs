using UnityEngine.SceneManagement;
using Zenject;

namespace GameSystem
{
    public class MainSceneLoader : IInitializable
    {
        public void Initialize()
        {
            LoadMainScene();
        }
        
        private void LoadMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
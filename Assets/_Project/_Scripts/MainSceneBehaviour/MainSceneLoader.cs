using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class MainSceneLoader
    {
        public MainSceneLoader()
        {
            LoadMainScene();
        }

        private void LoadMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}


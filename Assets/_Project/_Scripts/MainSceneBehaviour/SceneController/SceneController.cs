using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class SceneController
    {
        public void LoadScene(ScenesNames sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }

        public void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}

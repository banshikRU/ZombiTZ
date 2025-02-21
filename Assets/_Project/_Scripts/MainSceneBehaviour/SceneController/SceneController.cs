using _Project._Scripts.MainSceneBehaviour.SceneController;
using UnityEngine.SceneManagement;

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
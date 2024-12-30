using UnityEngine.SceneManagement;

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

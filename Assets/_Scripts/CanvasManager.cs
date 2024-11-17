using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _endGameMenu;

    private void OnEnable()
    {
        GameManager.OnGamePlayed += OnOffMainMenu;
        Player.OnPlayerDeath += EndGameMenu;
    }
    private void OnDisable()
    {
        GameManager.OnGamePlayed -= OnOffMainMenu;
        Player.OnPlayerDeath -= EndGameMenu;
    }
    private void OnOffMainMenu()
    {
        _mainMenu.SetActive(false);
    }
    private void EndGameMenu()
    {
        _endGameMenu.SetActive(true);
    }

}

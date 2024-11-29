using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _endGameMenu;
    [SerializeField] private GameStateUpdater _gameStateUpdater;
    [SerializeField] private PlayerBehaviour _player;

    private void OnEnable()
    {
        _gameStateUpdater.OnGamePlayed += OnOffMainMenu;
        _player.OnPlayerDeath += EndGameMenu;
    }
    private void OnDisable()
    {
        _gameStateUpdater.OnGamePlayed -= OnOffMainMenu;
        _player.OnPlayerDeath -= EndGameMenu;
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

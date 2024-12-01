using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _endGameMenu;
    [SerializeField] 
    private GameStateUpdater _gameStateUpdater;
    [SerializeField]
    private PlayerBehaviour _player;

    private void OnEnable()
    {
        _gameStateUpdater.OnGamePlayed += OffMainMenu;
        _player.OnPlayerDeath += OnEndGameMenu;
    }

    private void OnDisable()
    {
        _gameStateUpdater.OnGamePlayed -= OffMainMenu;
        _player.OnPlayerDeath -= OnEndGameMenu;
    }

    private void OffMainMenu()
    {
        _mainMenu.SetActive(false);
    }

    private void OnEndGameMenu()
    {
        _endGameMenu.SetActive(true);
    }

}

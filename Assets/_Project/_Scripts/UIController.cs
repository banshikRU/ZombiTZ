using UnityEngine;
using UnityEngine.UI;

public class UIController 
{
    private ScoresMenu _mainMenu;
    private ScoresMenu _endGameMenu;
    private GameStateUpdater _gameStateUpdater;
    private PlayerBehaviour _player;

    public UIController(ScoresMenu mainMenu, ScoresMenu endGameMenu, GameStateUpdater gameStateUpdater,PlayerBehaviour player)
    {
        _endGameMenu = endGameMenu;
        _mainMenu = mainMenu;
        _gameStateUpdater = gameStateUpdater;
        _player = player;

        EventInit();
    }

    private void EventInit()
    {
        _gameStateUpdater.OnGamePlayed += OffMainMenu;
        _player.OnPlayerDeath += OnEndGameMenu;
    }

    private void OffMainMenu()
    {
        _mainMenu.gameObject.SetActive(false);
    }

    private void OnEndGameMenu()
    {
        _endGameMenu.gameObject.SetActive(true);
    }

}

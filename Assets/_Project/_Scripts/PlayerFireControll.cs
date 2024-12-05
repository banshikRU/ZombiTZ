using UnityEngine;
using InputControll;

public class PlayerFireControll : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler _weaponHandler;
    [SerializeField]
    private BulletFabric _bulletFabric;
    [SerializeField]
    private GameStateUpdater _gameStateUpdater;
    [SerializeField]
    private InputHandler _inputHandler;
    
    private float _shootsInOneSeconds;
    private float _shootInSecond;
    private float _canShoot;

    private void OnEnable()
    {
        _inputHandler.currentInput.OnShoot += Shot;
        _gameStateUpdater.OnGamePlayed += TakeWeapon;
    }

    private void OnDisable()
    {
        _inputHandler.currentInput.OnShoot -= Shot;
        _gameStateUpdater.OnGamePlayed -= TakeWeapon;
    }

    private void TakeWeapon()
    {
        _weaponHandler.TakeWeapon();
        _shootsInOneSeconds = _weaponHandler.WeaponFireRate;
        _shootInSecond = _shootsInOneSeconds;
    }

    private void Shot()
    {
        if (!_gameStateUpdater.isGame) return;
        if ( Time.time >= _shootInSecond)
        {
            _shootInSecond = Time.time + _shootsInOneSeconds;
            _bulletFabric.Shot(Input.mousePosition);
        }
    }
}

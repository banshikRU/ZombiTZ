using UnityEngine;
using InputControll;
using GameStateControl;

namespace WeaponControl
{
    public class PlayerFireControll
    {
        private WeaponHandler _weaponHandler;
        private BulletFabric _bulletFabric;
        private GameStateUpdater _gameStateUpdater;
        private InputHandler _inputHandler;

        private float _shootsInOneSeconds;
        private float _shootInSecond;
        private float _canShoot;

        public PlayerFireControll(WeaponHandler weaponHandler, BulletFabric bulletFabric, GameStateUpdater gameStateUpdater, InputHandler inputHandler)
        {
            _weaponHandler = weaponHandler;
            _bulletFabric = bulletFabric;
            _gameStateUpdater = gameStateUpdater;
            _inputHandler = inputHandler;

        }

        public void Init()
        {
            _inputHandler.currentInput.OnShoot += Shot;
            _gameStateUpdater.OnGamePlayed += TakeWeapon;
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
            if (Time.time >= _shootInSecond)
            {
                _shootInSecond = Time.time + _shootsInOneSeconds;
                _bulletFabric.Shot(Input.mousePosition);
            }
        }
    }
}


using UnityEngine;
using GameStateControl;

namespace WeaponControl
{
    public class PlayerFireControl
    {
        private WeaponHandler _weaponHandler;
        private BulletFabric _bulletFabric;
        private GameStateUpdater _gameStateUpdater;

        private float _shootsInOneSeconds;
        private float _shootInSecond;

        public PlayerFireControl(WeaponHandler weaponHandler, BulletFabric bulletFabric, GameStateUpdater gameStateUpdater)
        {
            _weaponHandler = weaponHandler;
            _bulletFabric = bulletFabric;
            _gameStateUpdater = gameStateUpdater;

        }

        public void UnsubcribeEvent()
        {
            _gameStateUpdater.OnGamePlayed -= TakeWeapon;
        }

        public void Init()
        {
            _gameStateUpdater.OnGamePlayed += TakeWeapon;
        }

        private void TakeWeapon()
        {
            _weaponHandler.TakeWeapon();
            _shootsInOneSeconds = _weaponHandler.WeaponFireRate;
            _shootInSecond = _shootsInOneSeconds;
        }

        public void Shot()
        {
            if (!_gameStateUpdater.IsGame)
                return;
            if (Time.time >= _shootInSecond)
            {
                _shootInSecond = Time.time + _shootsInOneSeconds;
                _bulletFabric.Shot();
            }
        }
    }
}


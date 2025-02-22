using HelpUtilities;
using UnityEngine;
using Zenject;

namespace WeaponControl
{
    public class WeaponHandler :ITickable
    {
        private readonly Weapon _currentWeapon;
        private readonly Transform _player;
        private readonly float _weaponDistanceFromPlayer;

        public SpriteRenderer Weapon { get; private set; }

        public int BulletDamage
        {
            get
            {
                if (_currentWeapon == null)
                    return 0;
                return _currentWeapon.Bullet.BulletDamage;
            }
        }

        public float WeaponFireRate
        {
            get
            {
                if (_currentWeapon == null)
                    return 0;
                return _currentWeapon.ShootsInOneSecond;
            }

        }

        public WeaponHandler(Weapon currentWeapon, Transform player, float weaponDistanceFromPlayer,SpriteRenderer weapon)
        {
            Weapon = weapon;
            _currentWeapon = currentWeapon;
            _player = player;
            _weaponDistanceFromPlayer = weaponDistanceFromPlayer;
        }

        public void Tick()
        {
            RotateWeaponTowardsMouse();
        }

        public void TakeWeapon()
        {
           Weapon.sprite = _currentWeapon?.WeaponSprite;
        }

        private void RotateWeaponTowardsMouse()
        {
            Weapon.transform.position = _player.position + GetDirectionPlayerToMouse() * _weaponDistanceFromPlayer;
            float angle = Mathf.Atan2(GetDirectionPlayerToMouse().y, GetDirectionPlayerToMouse().x) * Mathf.Rad2Deg;
            Weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private Vector3 GetDirectionPlayerToMouse()
        {
            return (Utilities.GetWorldMousePosition() - _player.position).normalized;
        }
    }
}


using UnityEngine;

namespace WeaponControl
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class WeaponHandler
    {
        private Weapon _currentWeapon;
        private Transform _player;
        private float _weaponDistanceFromPlayer = 0.25f;

        public SpriteRenderer _weaponRenderer { get; private set; }

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

        public WeaponHandler(Weapon currentWeapon, Transform player, float weaponDistanceFromPlayer, SpriteRenderer renderer)
        {
            _currentWeapon = currentWeapon;
            _player = player;
            _weaponDistanceFromPlayer = weaponDistanceFromPlayer;
            _weaponRenderer = renderer;
        }

        public void Update()
        {
            RotateWeaponTowardsMouse();
        }

        public void TakeWeapon()
        {
            _weaponRenderer.sprite = _currentWeapon?.WeaponSprite;
        }

        private void RotateWeaponTowardsMouse()
        {
            _weaponRenderer.transform.position = _player.position + GetDirectionPlayerToMouse() * _weaponDistanceFromPlayer;
            float angle = Mathf.Atan2(GetDirectionPlayerToMouse().y, GetDirectionPlayerToMouse().x) * Mathf.Rad2Deg;
            _weaponRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private Vector3 GetDirectionPlayerToMouse()
        {
            return (Utilities.GetWorldMousePosition() - _player.position).normalized;
        }
    }
}


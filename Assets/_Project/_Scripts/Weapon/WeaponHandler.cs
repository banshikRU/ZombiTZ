using UnityEngine;

namespace WeaponControl
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class WeaponHandler : MonoBehaviour
    {
        private SpriteRenderer _weapon;
        private Weapon _currentWeapon;
        private Transform _player;
        private float _weaponDistanceFromPlayer;

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

        private void Awake()
        {
            _weapon = GetComponent<SpriteRenderer>();
        }

        public void Init(Weapon currentWeapon, Transform player, float weaponDistanceFromPlayer)
        {
            _currentWeapon = currentWeapon;
            _player = player;
            _weaponDistanceFromPlayer = weaponDistanceFromPlayer;
        }

        public void Update()
        {
            RotateWeaponTowardsMouse();
        }

        public void TakeWeapon()
        {
           _weapon.sprite = _currentWeapon?.WeaponSprite;
        }

        private void RotateWeaponTowardsMouse()
        {
            _weapon.transform.position = _player.position + GetDirectionPlayerToMouse() * _weaponDistanceFromPlayer;
            float angle = Mathf.Atan2(GetDirectionPlayerToMouse().y, GetDirectionPlayerToMouse().x) * Mathf.Rad2Deg;
            _weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private Vector3 GetDirectionPlayerToMouse()
        {
            return (Utilities.GetWorldMousePosition() - _player.position).normalized;
        }
    }
}


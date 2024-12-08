using UnityEngine;

namespace FireSystem
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class WeaponHandler : MonoBehaviour
    {
        private Weapon _currentWeapon;
        private Transform _player;
        private float _weaponDistanceFromPlayer = 0.25f;
        private bool isInit;
        private SpriteRenderer _renderer;

        public int BulletDamage
        {
            get
            {
                if (_currentWeapon == null) return 0;
                return _currentWeapon.Bullet.BulletDamage;
            }
        }

        public float WeaponFireRate
        {
            get
            {
                if (_currentWeapon == null) return 0;
                return _currentWeapon.ShootsInOneSecond;
            }

        }

        public void Init(Weapon currentWeapon, Transform player, float weaponDistanceFromPlayer)
        {
            _currentWeapon = currentWeapon;
            _player = player;
            _weaponDistanceFromPlayer = weaponDistanceFromPlayer;
            isInit = true;
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (isInit)
            {
                RotateWeaponTowardsMouse();
            }
        }

        public void TakeWeapon()
        {
            _renderer.sprite = _currentWeapon?.WeaponSprite;
        }

        private void RotateWeaponTowardsMouse()
        {
            transform.position = _player.position + GetDirectionPlayerToMouse() * _weaponDistanceFromPlayer;
            float angle = Mathf.Atan2(GetDirectionPlayerToMouse().y, GetDirectionPlayerToMouse().x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private Vector3 GetDirectionPlayerToMouse()
        {
            return (Utilities.GetWorldMousePosition() - _player.position).normalized;
        }
    }
}


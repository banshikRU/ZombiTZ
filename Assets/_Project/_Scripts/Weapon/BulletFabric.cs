using UnityEngine;
using ObjectPoolSystem;

namespace WeaponControl
{
    public class BulletFabric
    {
        private readonly ObjectPoolOrganizer _objectPoolOrganizer;
        private readonly BaseBullet _bullet;
        private readonly WeaponHandler _weaponHandler;
        private ObjectPool _objectPool;

        public BulletFabric(ObjectPoolOrganizer objectPoolOrganizer, BaseBullet bullet, WeaponHandler weaponHandler)
        {
            _objectPoolOrganizer = objectPoolOrganizer;
            _bullet = bullet;
            _weaponHandler = weaponHandler;

            Init();
        }

        public void Init()
        {
            _objectPool = _objectPoolOrganizer.GetPool(_bullet.gameObject.name);
        }

        public void Shot()
        {
            BulletSetUp(TakeBulletFromPool());
        }

        private Vector2 DirectionDefine()
        {
            return (Utilities.GetWorldMousePosition() - _weaponHandler.Weapon.transform.position).normalized;
        }

        private GameObject TakeBulletFromPool()
        {
            GameObject bulletObject = _objectPool.GetObject().gameObject;
            if (bulletObject == null)
                return null;
            return bulletObject;
        }

        private void BulletSetUp(GameObject bullet)
        {
            bullet.SetActive(true);
            bullet.transform.SetPositionAndRotation(_weaponHandler.Weapon.transform.position, Quaternion.identity);
            BaseBullet baseBullet = bullet.GetComponent<BaseBullet>();
            baseBullet.StartMoveBullet(DirectionDefine(), _weaponHandler.BulletDamage);
        }
    }
}


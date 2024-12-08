using UnityEngine;

namespace FireSystem
{
    public class BulletFabric
    {
        private ObjectPoolOrganizer _objectPoolOrganizer;
        private BaseBullet _bullet;
        private WeaponHandler _weaponHandler;

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

        public void Shot(Vector3 movingVector)
        {
            BulletSetUp(TakeBulletFromPool(), movingVector);
        }

        private Vector2 DirectionDefine(Vector3 vector)
        {
            return (Utilities.GetWorldMousePosition() - _weaponHandler.transform.position).normalized;
        }

        private GameObject TakeBulletFromPool()
        {
            GameObject bulletObject = _objectPool.GetPooledObject().gameObject;
            if (bulletObject == null)
                return null;
            return bulletObject;
        }

        private void BulletSetUp(GameObject bullet, Vector3 movingVector)
        {
            bullet.SetActive(true);
            bullet.transform.SetPositionAndRotation(_weaponHandler.transform.position, Quaternion.identity);
            BaseBullet baseBullet = bullet.GetComponent<BaseBullet>();
            baseBullet.StartMoveBullet(DirectionDefine(movingVector), _weaponHandler.BulletDamage);
        }
    }
}


using UnityEngine;
using ObjectPoolSystem;
using Firebase;
using System;

namespace WeaponControl
{
    public class BulletFabric
    {
        public event Action<VFXEvent,SFXType> OnBulletShot;

        private readonly ObjectPoolOrganizer _objectPoolOrganizer;
        private readonly BaseBullet _bullet;
        private readonly WeaponHandler _weaponHandler;
        private readonly AnalyticsDataCollector _analyticsDataCollector;
        private ObjectPool _objectPool;

        public BulletFabric(ObjectPoolOrganizer objectPoolOrganizer, BaseBullet bullet, WeaponHandler weaponHandler, AnalyticsDataCollector analyticsDataCollector)
        {
            _objectPoolOrganizer = objectPoolOrganizer;
            _bullet = bullet;
            _weaponHandler = weaponHandler;
            _analyticsDataCollector = analyticsDataCollector;

            Init();
        }

        public void Init()
        {
            _objectPool = _objectPoolOrganizer.GetPool(_bullet.gameObject.name);
        }

        public void Shot()
        {
            OnBulletShot.Invoke(new VFXEvent(_weaponHandler.Weapon.transform.position, Quaternion.identity, VFXTypes.BulletFire),SFXType.PistolShot);
            _analyticsDataCollector.AddAnalizedParameterValue(_bullet.name, 1);
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


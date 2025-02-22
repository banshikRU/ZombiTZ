using UnityEngine;
using ObjectPoolSystem;
using Firebase.Analytics;
using System;
using FXSystem;
using HelpUtilities;
using Zenject;

namespace WeaponControl
{
    public class BulletFabric: IInitializable,IFXEventSender
    {
        public event Action<Vector3, IFXEventSender> OnFXEvent;
        public event Action OnShot;

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
        }

        public void Initialize()
        {
            _objectPool = _objectPoolOrganizer.GetPool(_bullet.gameObject.name);
        }

        public void Shot()
        {
            OnShot?.Invoke();   
            OnFXEvent?.Invoke(_weaponHandler.Weapon.transform.position,this);
            _analyticsDataCollector.AddAnalyzedParameterValue(_bullet.name, 1);
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
            BaseBullet baseBullet = bullet.GetComponent<BaseBullet>();
            baseBullet.StartMoveBullet(_weaponHandler.Weapon.transform.position,DirectionDefine(), _weaponHandler.BulletDamage);
        }
    }
}


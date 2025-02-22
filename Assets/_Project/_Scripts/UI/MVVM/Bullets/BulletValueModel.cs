using System;
using UniRx;
using WeaponControl;
using Zenject;

namespace UIControl.MVVM.Bullets
{
    public class BulletValueModel: IDisposable,IInitializable
    {
        private readonly BulletFabric _bulletFabric;

        public readonly ReactiveProperty<int> Bullets = new();

        public BulletValueModel(BulletFabric bulletFabric)
        {
            _bulletFabric = bulletFabric;
        }
        
        public void Initialize()
        {
            SubscribeEvent();
        }
        
        public void Dispose()
        {
            _bulletFabric.OnShot -= OnShot;
        }
        
        private void SubscribeEvent()
        {
            _bulletFabric.OnShot += OnShot;
        }
        
        private void OnShot()
        {
            Bullets.Value++;
        }
    }
}
using UniRx;
using WeaponControl;

namespace UIControl.MVVM.Bullets
{
    public class BulletValueModel
    {
        private BulletFabric _bulletFabric;

        public readonly ReactiveProperty<int> Bullets = new();

        public BulletValueModel(BulletFabric bulletFabric)
        {
            _bulletFabric = bulletFabric;
            
            SubscribeEvent();
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
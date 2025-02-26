using System;
using UIControl.MVVM.Bullets;
using UniRx;
using Zenject;

public class BulletViewModel : IDisposable,IInitializable
{
    public readonly ReactiveProperty<int> Bullets = new();
    
    private readonly BulletValueModel _bulletValueModel;

    public BulletViewModel(BulletValueModel bulletValueModel)
    {
        _bulletValueModel = bulletValueModel;
    }
    
    public void Dispose()
    {
        UnsubscribeEvents();
    }

    public void Initialize()
    {
        OnBulletChanged(_bulletValueModel.Bullets.Value);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _bulletValueModel.Bullets.Subscribe(OnBulletChanged);
    }

    private void UnsubscribeEvents()
    {
        _bulletValueModel.Bullets.Dispose();
    }

    private void OnBulletChanged(int value)
    {
        Bullets.Value = value;
    }
}

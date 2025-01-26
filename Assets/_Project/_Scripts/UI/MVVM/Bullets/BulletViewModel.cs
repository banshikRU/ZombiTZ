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
        _bulletValueModel.Bullets.Dispose();
    }

    public void Initialize()
    {
        OnBulletChanged(_bulletValueModel.Bullets.Value);
        _bulletValueModel.Bullets.Subscribe(OnBulletChanged);
    }

    private void OnBulletChanged(int value)
    {
        Bullets.Value = value;
    }
}

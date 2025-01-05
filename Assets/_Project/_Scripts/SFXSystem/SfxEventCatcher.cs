using System;
using WeaponControl;
using ZombieGeneratorBehaviour;

public class SfxEventCatcher : IDisposable
{
    public event Action<SFXType> OnsSFXRequested;

    private readonly BulletFabric _bulletFabric;
    private readonly ZombieFactory _zombieFactory;

    public SfxEventCatcher(BulletFabric bulletFabric, ZombieFactory zombieFactory)
    {
        _zombieFactory = zombieFactory;
        _bulletFabric = bulletFabric;
        EventInit();
    }

    public void Dispose()
    {
        _bulletFabric.OnBulletShot -= RequestSFX;
        _zombieFactory.OnZombieDie += RequestSFX;
    }

    public void RequestSFX(VFXEvent vFX,SFXType SfxEvent)
    {
        OnsSFXRequested?.Invoke(SfxEvent);
    }

    private void EventInit()
    {
        _zombieFactory.OnZombieDie += RequestSFX;
        _bulletFabric.OnBulletShot += RequestSFX;
    }
}

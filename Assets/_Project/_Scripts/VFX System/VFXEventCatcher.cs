using System;
using WeaponControl;
using ZombieGeneratorBehaviour;

public class VFXEventCatcher : IDisposable
{
    public event Action<VFXEvent> OnVFXRequested;

    private readonly BulletFabric _bulletFabric;
    private readonly ZombieFactory _zombieFactory;

    public VFXEventCatcher(BulletFabric bulletFabric,ZombieFactory zombieFactory)
    {
        _zombieFactory = zombieFactory;
        _bulletFabric = bulletFabric;
        EventInit();
    }

    public void Dispose()
    {
        _bulletFabric.OnBulletShot -= RequestVFX;
        _zombieFactory.OnZombieDie += RequestVFX;
    }

    public void RequestVFX(VFXEvent vfxEvent,SFXType sfxType)
    {
        OnVFXRequested?.Invoke(vfxEvent);
    }

    private void EventInit()
    {
        _zombieFactory.OnZombieDie += RequestVFX;
        _bulletFabric.OnBulletShot += RequestVFX;
    }

}

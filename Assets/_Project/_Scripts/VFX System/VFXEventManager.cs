using System;
using WeaponControl;
using ZombieGeneratorBehaviour;

public class VFXEventManager : IDisposable,IVFXEventManager
{
    public event Action<VFXEvent> OnVFXRequested;

    private readonly BulletFabric _bulletFabric;
    private readonly ZombieFactory _zombieFactory;

    public VFXEventManager(BulletFabric bulletFabric,ZombieFactory zombieFactory)
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

    public void RequestVFX(VFXEvent vfxEvent)
    {
        OnVFXRequested?.Invoke(vfxEvent);
    }

    private void EventInit()
    {
        _zombieFactory.OnZombieDie += RequestVFX;
        _bulletFabric.OnBulletShot += RequestVFX;
    }

}

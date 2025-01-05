using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class VFXManager:IDisposable
{
    private readonly List<VFXBehaviour> _vfxPrefabs;
    private readonly VFXEventManager _vfxEventManager;

    public VFXManager(VFXEventManager vfxEventManager,List<VFXBehaviour> vfxPrefabs)
    {
        _vfxPrefabs = vfxPrefabs;
        _vfxEventManager = vfxEventManager;
        EventInit();
    }

    public void Dispose()
    {
        _vfxEventManager.OnVFXRequested -= PlayVFX;
    }

    public void PlayVFX(VFXEvent vfxEvent)
    {
        foreach (VFXBehaviour VFX in _vfxPrefabs)
        {
            if (VFX.VFXType == vfxEvent.VFXType)
            {
                Object.Instantiate(VFX, vfxEvent.Position, vfxEvent.Rotation);
                break;
            }
        }
    }

    private void EventInit()
    {
        _vfxEventManager.OnVFXRequested += PlayVFX;
    }
}

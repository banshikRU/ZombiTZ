using System;

namespace VFXSystem
{
    public interface IVFXEventManager
    {
        public event Action<VFXEvent> OnVFXRequested;
        public void RequestVFX(VFXEvent vfxEvent);
    }

}

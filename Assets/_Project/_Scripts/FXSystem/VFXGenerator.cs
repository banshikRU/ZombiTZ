using UnityEngine;
using Object = UnityEngine.Object;

namespace FXSystem
{
    public class VFXGenerator 
    {
        public void PlayVFX(GameObject vfxPrefab, Vector3 vfxPosition,Quaternion vfxRotation)
        {
            Object.Instantiate(vfxPrefab, vfxPosition, vfxRotation);
        }
    }
}

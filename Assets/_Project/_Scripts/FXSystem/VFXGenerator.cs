using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VFXSystem
{
    public class VFXGenerator 
    {
        public void PlayVFX(GameObject vfxPrefab, Vector3 vfxPosition,Quaternion vfxRotation)
        {
            Object.Instantiate(vfxPrefab, vfxPosition, vfxRotation);
        }

    }
}

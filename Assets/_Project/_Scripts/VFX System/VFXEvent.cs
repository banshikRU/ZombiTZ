using UnityEngine;

namespace VFXSystem
{
    public class VFXEvent
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public VFXTypes VFXType { get; }

        public VFXEvent(Vector3 position, Quaternion rotation, VFXTypes vfxType)
        {
            Position = position;
            Rotation = rotation;
            VFXType = vfxType;
        }
    }
}

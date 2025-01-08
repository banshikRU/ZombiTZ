using UnityEngine;

namespace VFXSystem
{
    public class VFXEvent
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public VFXType VFXType { get; }

        public VFXEvent(Vector3 position, Quaternion rotation, VFXType vfxType)
        {
            Position = position;
            Rotation = rotation;
            VFXType = vfxType;
        }
    }
}

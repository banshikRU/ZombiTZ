using UnityEngine;

public partial class ObjectPoolOrganizer : MonoBehaviour
{
    [System.Serializable]
    public class PoolConfig
    {
        public PooledObject prefab;
        public uint initialSize;
    }
}

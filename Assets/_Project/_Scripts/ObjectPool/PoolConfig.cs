namespace ObjectPoolSystem
{
    public partial class ObjectPoolOrganizer
    {
        [System.Serializable]
        public class PoolConfig
        {
            public PooledObject prefab;
            public uint initialSize;
        }
    }

}

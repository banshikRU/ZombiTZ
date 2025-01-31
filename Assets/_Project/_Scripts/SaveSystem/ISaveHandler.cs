namespace SaveSystem
{
    public interface ISaveHandler<T>
    {
        public T  PlayerDataValues{get;set;}
        public void SaveData();
        public T LoadData();
    }
}
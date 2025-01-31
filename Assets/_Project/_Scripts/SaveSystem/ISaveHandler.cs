namespace SaveSystem
{
    public interface ISaveHandler<out T>
    {
        public void SaveData();
        public T LoadData();
    }
}
using System.Threading.Tasks;

namespace SaveSystem
{
    public interface ISaveService
    {
        Task SaveAsync(string key, string data);
        Task<string> LoadAsync(string key);
    }
}

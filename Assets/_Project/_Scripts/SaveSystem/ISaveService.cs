using System.Threading.Tasks;

public interface ISaveService
{
    Task SaveAsync(string key, string data);
    Task<string> LoadAsync(string key);
}
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace SaveSystem
{
    public interface ISaveService
    {
        UniTask SaveAsync(string key, string data);
        UniTask<string> LoadAsync(string key);
    }
}

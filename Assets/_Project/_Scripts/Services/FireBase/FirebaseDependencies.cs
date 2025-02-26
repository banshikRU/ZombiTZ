using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine;

namespace Firebase
{
    public class FirebaseDependencies
    {
        public async UniTask Initialize()
        {
            await CheckFirebaseDependencies();
        }

        private UniTask CheckFirebaseDependencies()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    //Debug.Log("All Dependencies Sync");
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
            return UniTask.CompletedTask;
        }
    }
}


using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace Firebase
{
    public class FirebaseDependencies: IInitializable
    {
        public void Initialize()
        {
            CheckFirebaseDependencies();
        }

        private void CheckFirebaseDependencies()
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
        }
    }
}


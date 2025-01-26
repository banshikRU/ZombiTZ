using Firebase.Extensions;
using UnityEngine;

namespace Firebase
{
    public class FirebaseDependencies
    {
        public FirebaseDependencies()
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


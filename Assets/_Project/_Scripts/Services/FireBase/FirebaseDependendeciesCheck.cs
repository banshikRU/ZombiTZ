using Firebase.Extensions;
using UnityEngine;

public class FirebaseDependendeciesCheck 
{
    public FirebaseDependendeciesCheck()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                //Debug.Log("All Dependencies Sync");
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });

    }
}

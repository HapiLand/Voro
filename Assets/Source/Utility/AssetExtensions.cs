using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Source.Utility {
public static class AssetExtensions {
    public static void LoadAssetPath<T>(string pathTo, Action<T> onLoaded) where T : Object {
        Addressables.LoadAssetAsync<T>(pathTo).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                onLoaded?.Invoke(handle.Result);
            }
        };
    }
}
}
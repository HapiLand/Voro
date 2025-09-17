using System;
using System.Collections.Generic;
using OLD.VoroEditor.GUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace EditorGUI.Source.Utility {
public static class AssetHelper {
    static readonly Dictionary<EffectMaster, KeyValuePair<string, string>> EffectMenuMap =
        new()
        {
            { EffectMaster.Slope, new KeyValuePair<string, string>("Generation/Slope", "Slope") },
            { EffectMaster.SetHeight, new KeyValuePair<string, string>("Generation/SetHeight", "SetHeight") },
            { EffectMaster.Noise, new KeyValuePair<string, string>("Generation/Noise", "Noise") },
            { EffectMaster.Terrace, new KeyValuePair<string, string>("Manipulate/Terrace", "Terrace") },
            { EffectMaster.SetTag, new KeyValuePair<string, string>("Tags/SetTag", "SetTag") }
        };

    public static void LoadAllAssetsInGroupName<T>(string groupLabel, Action<IList<T>> onLoaded) where T : Object {
        Addressables.LoadAssetsAsync<T>(groupLabel).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                onLoaded?.Invoke(handle.Result);
            }
            else {
                Debug.LogError($"Failed to load assets with label: {groupLabel}");
                onLoaded?.Invoke(null);
            }
        };
    }

    public static void LoadAssetPath<T>(string pathTo, Action<T> onLoaded) where T : Object {
        Addressables.LoadAssetAsync<T>(pathTo).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                onLoaded?.Invoke(handle.Result);
            }
            else {
                Debug.LogError($"AssetHelper failed to load asset at path: {pathTo}");
                onLoaded?.Invoke(null);
            }
        };
    }


    public static KeyValuePair<string, string> GetEffectMenuPath(EffectMaster effect) {
        // ToDo <string,EffectVisualElement>
        return EffectMenuMap.TryGetValue(effect, out var value)
            ? value
            : new KeyValuePair<string, string>("Misc/Null", "Null");
    }
}
}
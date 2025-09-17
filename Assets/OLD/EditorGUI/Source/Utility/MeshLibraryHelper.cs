using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace EditorGUI.Source.Utility {
/// <summary>
///     utility class for accessing the MeshLibrary for use with a WorldTile.VoroDiagram.CellPoint
/// </summary>
public class MeshLibraryHelper {
    static readonly Lazy<MeshLibraryHelper> _lazyInstance = new(() => new MeshLibraryHelper());
    readonly Dictionary<int, Mesh[]> _meshMap;

    MeshLibraryHelper() {
        Debug.Log("MeshLibraryHelper creating now");
        _meshMap = new Dictionary<int, Mesh[]>();

        // 
        CreateMeshMapDictionary();


        void CreateMeshMapDictionary() {
            var meshLibrary = LoadAssetsFromLibrary();

            List<GameObject> LoadAssetsFromLibrary() {
                var settings = AddressableAssetSettingsDefaultObject.Settings;
                var group = settings.FindGroup("MeshLibrary");
                if (group == null) {
                    Debug.LogError("MeshLibrary group not found.");
                    return null;
                }

                var loadedAssets = new List<GameObject>();

                foreach (var entry in group.entries) {
                    var address = entry.address;
                    var asset = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(entry.guid));
                    if (asset != null) {
                        loadedAssets.Add(asset);
                    }
                }

                Debug.Log($"Loaded {loadedAssets.Count} assets from MeshLibrary.");
                return loadedAssets;
            }

            var tempMap = new Dictionary<int, List<Mesh>>(); // temporary storage

            foreach (var meshObject in meshLibrary) {
                var parts = meshObject.name.Split('_');
                if (parts.Length != 2 || !int.TryParse(parts[0], out var key) ||
                    !int.TryParse(parts[1], out var variant)) {
                    // skip as the name is invalid, format is 0_0.fbx
                    continue;
                }

                // key value matches the ID of each CellPoint
                if (!tempMap.ContainsKey(key)) {
                    tempMap[key] = new List<Mesh>();
                }

                // read the mesh instance from the GameObject
                var meshFilter = meshObject.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.sharedMesh != null) {
                    tempMap[key].Add(meshFilter.sharedMesh);
                }

                // construct the actual mesh dictionary
                // ID | [Mesh,Mesh,Mesh]
                foreach (var kvp in tempMap) {
                    _meshMap[kvp.Key] = kvp.Value.ToArray();
                }
            }
        }
    }

    public static MeshLibraryHelper Instance => _lazyInstance.Value;

    public Mesh[] GetMeshArray(int id) {
        return _meshMap[id];
    }
}
}

// piece 0
//  variant 0
//  variant 1
//  variant 2
// piece 1
//  variant 0
//  variant 1
//  variant 2
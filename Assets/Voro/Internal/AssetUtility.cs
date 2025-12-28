using System.IO;
using UnityEditor;
using UnityEngine;

namespace Voro.Internal {
public static class AssetUtility {
  public const string BasePath = "Assets/Voro/Persistence";

  public static bool DoesDirectoryExist(string path) {
    return Directory.Exists(GetFullPath(path));
  }

  public static void CreateDirectory(string path) {
    path = GetFullPath(path);
    if (Directory.Exists(path)) {
      return;
    }
    Directory.CreateDirectory(path);
    AssetDatabase.Refresh();
  }

  public static void RemoveDirectory(string path) {
    path = GetFullPath(path);
    if (!Directory.Exists(path)) {
      return;
    }
    FileUtil.DeleteFileOrDirectory(path);
    FileUtil.DeleteFileOrDirectory($"{path}.meta");
    AssetDatabase.Refresh();
  }

  static string GetFullPath(string path) {
    path = $"{BasePath}/{path}";
    return path;
  }

  public static void CreateOrOverwriteAsset<T>(string path, T asset) where T : ScriptableObject {
    var assetPath = GetAssetPath<T>(path);
    CreateAssetDirectory(path); // ensure directory exists
    CreateAsset(assetPath, out asset);
  }
  
  public static void GetOrCreateAsset<T>(string path, out T asset) where T : ScriptableObject {
    var assetPath = GetAssetPath<T>(path);
    asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

    if (asset != null) {
      return;
    }

    Debug.Log($"Asset Type '{typeof(T).Name}' not found, creating.");
    CreateAssetDirectory(path); // ensure directory exists
    CreateAsset(assetPath, out asset);
  }

  public static void RemoveAsset<T>(string path) where T : ScriptableObject {
    var assetPath = GetAssetPath<T>(path);
    if (!DoesAssetExist<T>(path)) {
      return;
    }

    FileUtil.DeleteFileOrDirectory(assetPath);
    FileUtil.DeleteFileOrDirectory($"{assetPath}.meta");
    AssetDatabase.Refresh();
  }

  static bool DoesAssetExist<T>(string path) where T : ScriptableObject {
    var assetPath = GetAssetPath<T>(path);
    return AssetDatabase.LoadAssetAtPath<T>(assetPath) != null;
  }

  static string GetAssetPath<T>(string path) where T : ScriptableObject {
    return $"{GetFullPath(path)}/{typeof(T).Name}.asset";
  }

  static void CreateAssetDirectory(string path) {
    CreateDirectory(path);
  }

  static void CreateAsset<T>(string assetPath, out T asset) where T : ScriptableObject {
    asset = ScriptableObject.CreateInstance<T>();
    AssetDatabase.CreateAsset(asset, assetPath);
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
  }
}
}
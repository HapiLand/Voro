using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Voro.UserInterface.Internal.TerrainOperators.Editor {
public static class OpAssetUtility {
  public static List<Operator> GetAssetList() {
    var guids = AssetDatabase.FindAssets("t:Operator", new[] { OpAssetPaths.SourcePath });
    return guids
      .Select(AssetDatabase.GUIDToAssetPath)
      .Select(AssetDatabase.LoadAssetAtPath<Operator>)
      .Where(asset => asset != null)
      .ToList();
  }

  public static void SaveAsset(Operator asset) {
    var path = $"{OpAssetPaths.SourcePath}/{asset.title}.asset";
    AssetDatabase.CreateAsset(asset, path);
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
  }

  public static string GetAssetPath(string title) {
    return $"{OpAssetPaths.SourcePath}/{title}.asset";
  }

  public static bool DoesAssetExist(string title) {
    return AssetDatabase.LoadAssetAtPath<Operator>($"{OpAssetPaths.SourcePath}/{title}.asset") != null;
  }
}
}
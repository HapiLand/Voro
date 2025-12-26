using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Voro.UserInterface.Internal.TerrainComputes.Editor {
public static class ComputeAssetUtility {
  public static List<Compute> GetAssetList() {
    var guids = AssetDatabase.FindAssets("t:Compute", new[] { ComputeAssetPaths.SourcePath });
    return guids
      .Select(AssetDatabase.GUIDToAssetPath)
      .Select(AssetDatabase.LoadAssetAtPath<Compute>)
      .Where(asset => asset != null)
      .ToList();
  }

  public static void SaveAsset(Compute asset) {
    var path = $"{ComputeAssetPaths.SourcePath}/{asset.kernel}.asset";
    AssetDatabase.CreateAsset(asset, path);
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
  }

  public static string GetAssetPath(string title) {
    return $"{ComputeAssetPaths.SourcePath}/{title}.asset";
  }

  public static bool DoesAssetExist(string title) {
    return AssetDatabase.LoadAssetAtPath<Compute>($"{ComputeAssetPaths.SourcePath}/{title}.asset") != null;
  }
}
}
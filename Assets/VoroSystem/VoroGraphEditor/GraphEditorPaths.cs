// ReSharper disable InconsistentNaming

using System.IO;
using UnityEngine;

#if UNITY_EDITOR
namespace VoroSystem.VoroGraphEditor {
public static class GraphEditorPaths {
  public const string SCRIPTABLE_OBJECT_ASSET_RELATIVE_PATH =
    "VoroSystem/Voro.Persistence/Resources/ScriptableObject/GraphData.asset";

  public const string JSON_ASSET_RELATIVE_PATH =
    "VoroSystem/Voro.Persistence/Resources/JSON/GraphPresets/graph.json";

  public static string ScriptableObjectAssetPath =>
    Path.Combine(Application.dataPath, SCRIPTABLE_OBJECT_ASSET_RELATIVE_PATH);

  public static string JsonAssetPath =>
    Path.Combine(Application.dataPath, JSON_ASSET_RELATIVE_PATH);
}
}
#endif
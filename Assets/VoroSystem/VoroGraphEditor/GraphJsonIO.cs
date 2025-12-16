#if UNITY_EDITOR
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using VoroSystem.VoroGraphEditor.Data;

namespace VoroSystem.VoroGraphEditor {
public static class GraphJsonIO {
  public static void ImportFromJson(GraphScriptableObject target) {
    var path = GraphEditorPaths.JsonAssetPath;

    if (!File.Exists(path)) {
      Debug.LogError($"JSON file not found: {path}");
      return;
    }

    var json = File.ReadAllText(path);
    var dataObject = JsonConvert.DeserializeObject<GraphDataDTO>(json);

    Undo.RecordObject(target, "Import JSON");
    GraphMapper.ApplyToScriptableObject(dataObject, target);
    EditorUtility.SetDirty(target);
  }

  public static void ExportToJson(GraphScriptableObject source) {
    var path = GraphEditorPaths.JsonAssetPath;
    var dataObject = GraphMapper.ToDataObject(source);
    var json = JsonConvert.SerializeObject(dataObject, Formatting.Indented);

    // ensure directory exists
    Directory.CreateDirectory(Path.Combine(Application.dataPath,
      Path.GetDirectoryName(GraphEditorPaths.JSON_ASSET_RELATIVE_PATH)!));

    File.WriteAllText(path, json);
    AssetDatabase.Refresh();

    Debug.Log($"Exported JSON to {path}");
  }
}
}
#endif
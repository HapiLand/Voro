#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using VoroSystem.VoroGraphEditor.Data;

namespace VoroSystem.VoroGraphEditor {
public static class GraphScriptableObjectUtility {
  
  public static GraphScriptableObject GetOrCreate() {
    
    var so = AssetDatabase.LoadAssetAtPath<GraphScriptableObject>(
      "Assets/" + GraphEditorPaths.SCRIPTABLE_OBJECT_ASSET_RELATIVE_PATH
    );

    if (so != null) {
      return so;
    }

    // ensure directory exists
    Directory.CreateDirectory(
      Path.Combine(Application.dataPath,
        Path.GetDirectoryName(GraphEditorPaths.SCRIPTABLE_OBJECT_ASSET_RELATIVE_PATH)!)
    );

    // create new instance
    so = ScriptableObject.CreateInstance<GraphScriptableObject>();

    AssetDatabase.CreateAsset(
      so,
      "Assets/" + GraphEditorPaths.SCRIPTABLE_OBJECT_ASSET_RELATIVE_PATH
    );

    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();

    Debug.Log(
      $"Created new ScriptableObject at: {GraphEditorPaths.SCRIPTABLE_OBJECT_ASSET_RELATIVE_PATH}"
    );

    return so;
  }
}
}
#endif
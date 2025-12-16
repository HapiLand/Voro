#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VoroSystem.VoroGraphEditor.Data;

namespace VoroSystem.VoroGraphEditor.Editor {
public class GraphEditorWindow : EditorWindow {
  GraphScriptableObject _graphData;
  SerializedObject _serializedGraphData;

  #region Event Functions
  void OnEnable() {
    _graphData = GraphScriptableObjectUtility.GetOrCreate();
    _serializedGraphData = new SerializedObject(_graphData);
  }

  void OnGUI() {
    DrawScriptableObjectField();

    if (_graphData == null) {
      EditorGUILayout.HelpBox(
        "graph data could not be loaded or created",
        MessageType.Error
      );
      return;
    }

    DrawEditorFields();
    GUILayout.Space(10);
    DrawImportExportButtons();
  }
  #endregion

  [MenuItem("VoroNew/Graph Editor")]
  public static void ShowWindow() {
    var wnd = GetWindow<GraphEditorWindow>();
    wnd.titleContent = new GUIContent("Graph Editor");
  }

  void DrawScriptableObjectField() {
    var newData = (GraphScriptableObject)EditorGUILayout.ObjectField(
      "Graph Data",
      _graphData,
      typeof(GraphScriptableObject),
      false
    );

    if (newData != _graphData) {
      _graphData = newData;
      _serializedGraphData = _graphData ? new SerializedObject(_graphData) : null;
    }
  }

  void DrawEditorFields() {
    _serializedGraphData.Update();

    EditorGUILayout.LabelField("Graph Data", EditorStyles.boldLabel);
    EditorGUILayout.PropertyField(_serializedGraphData.FindProperty("graphName"));

    // draw layer list
    EditorGUILayout.PropertyField(_serializedGraphData.FindProperty("layers"), true);
    _serializedGraphData.ApplyModifiedProperties();
  }

  void DrawImportExportButtons() {
    GUILayout.Space(10);

    if (GUILayout.Button("Import JSON")) {
      GraphJsonIO.ImportFromJson(_graphData);
    }

    if (GUILayout.Button("Export JSON")) {
      GraphJsonIO.ExportToJson(_graphData);
    }
  }
}
}
#endif
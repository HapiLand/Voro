#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.GraphEditor.Data;

namespace VoroSystem.Voro.GraphEditor.Editor {
public class GraphComponentEditorWindow : EditorWindow {
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

  [MenuItem("VoroTools/Graph Editor")]
  public static void ShowWindow() {
    var wnd = GetWindow<GraphComponentEditorWindow>();
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
    // EditorGUILayout.PropertyField(_serializedGraphData.FindProperty("Number"));
    // EditorGUILayout.PropertyField(_serializedGraphData.FindProperty("Toggle"));
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
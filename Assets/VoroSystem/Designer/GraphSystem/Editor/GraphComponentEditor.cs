using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Compute.EffectSystem.Core;

namespace VoroSystem.Designer.GraphSystem.Editor {
[CustomEditor(typeof(GraphComponent))]
public class GraphComponentEditor : UnityEditor.Editor {
  #region Serialized Fields

  [SerializeReference] GraphComponent graphComponent;
  [SerializeReference] SerializedProperty graphProp;
  [SerializeReference] string newLayerName = "";
  [SerializeReference] int selectedID;

  #endregion

  #region Event Functions

  void OnEnable() {
    graphComponent = target as GraphComponent;
    if (graphComponent != null) {
      graphProp = serializedObject.FindProperty("graph");
    }
  }

  void OnDisable() {
    graphComponent = null;
    graphProp = null;
  }

  #endregion

  public override void OnInspectorGUI() {
    if (!graphComponent) {
      EditorGUILayout.HelpBox("GraphComponent is null or has been destroyed.", MessageType.Warning);
      return;
    }

    serializedObject.Update();

    if (graphComponent.graph == null) {
      EditorGUILayout.HelpBox("Graph is null.", MessageType.Info);
      serializedObject.ApplyModifiedProperties();
      return;
    }

    GUILayout.Label($"Graph: {graphComponent.graph.graphName}");
    EditorGUILayout.BeginHorizontal();
    newLayerName = EditorGUILayout.TextField("New Layer Name:", newLayerName);
    if (GUILayout.Button($"Create Layer \"{newLayerName}\"")) {
      if (!string.IsNullOrWhiteSpace(newLayerName)) {
        graphComponent.graph.CreateLayer(newLayerName);
      }
    }

    EditorGUILayout.EndHorizontal();
    DrawUILine(Color.cornflowerBlue);
    LayersGUI();

    serializedObject.ApplyModifiedProperties();
    return;

    void LayersGUI() {
      foreach (var layer in graphComponent.graph.layers) {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Layer \"{layer.layerName}\"");

        NodeGUI(layer.nodes);

        var names = NodeLookup.Names.ToList();
        selectedID = EditorGUILayout.Popup("Select Effect", selectedID, names.ToArray());
        if (GUILayout.Button("Add Effect")) {
          var selected = names[selectedID];
          var def = NodeLookup.Get(selected);
          layer.CreateNode(def);
        }

        EditorGUILayout.EndVertical();
        DrawUILine(Color.darkOrange);
      }
    }

    void NodeGUI(List<Node> nodes) {
      foreach (var node in nodes) {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Effect \"{node.nodeName}\"");
        node.operation = (EffectOperation)EditorGUILayout.EnumPopup("Operation:", node.operation);
        FieldsGUI(node);
        EditorGUILayout.EndVertical();
        DrawUILine(Color.springGreen);
      }
    }

    void FieldsGUI(Node node) {
      foreach (var field in node.fields) {
        GUILayout.BeginHorizontal();
        field.DrawGUI();
        GUILayout.EndHorizontal();
      }
    }

    void DrawUILine(Color color, int thickness = 1, int padding = 10) {
      var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
      r.height = thickness;
      r.y += padding / 2f;
      r.x -= 2;
      r.width += 6;
      EditorGUI.DrawRect(r, color);
    }
  }
}
}
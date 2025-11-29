using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.V2;

namespace VoroSystem.Voro.Compute.Editor {
[CustomEditor(typeof(VoroDiagram))]
public class VoroDiagramEditor : UnityEditor.Editor {
  #region Serialized Fields

  [SerializeReference] SerializedProperty serializedProperty;
  [SerializeReference] string newLayerName = "";
  [SerializeReference] NodeType newNodeType = NodeType.Debug;
  [SerializeReference] VoroDiagram voroDiagram;
  [SerializeReference] Diagram diagram;

  #endregion

  #region Event Functions

  void OnEnable() {
    voroDiagram = target as VoroDiagram;
    if (voroDiagram == null) {
      return;
    }

    serializedProperty = serializedObject.FindProperty("diagram");
    diagram = voroDiagram.diagram;
  }

  void OnDisable() {
    voroDiagram = null;
    serializedProperty = null;
  }

  #endregion

  public override void OnInspectorGUI() {
    if (!voroDiagram) {
      return;
    }

    serializedObject.Update();

    GUILayout.Label($"Diagram: {diagram.diagramName}");
    EditorGUILayout.BeginHorizontal();
    newLayerName = EditorGUILayout.TextField("New Layer Name:", newLayerName);
    if (GUILayout.Button($"Create Layer \"{newLayerName}\"")) {
      voroDiagram.graph.CreateLayer(!string.IsNullOrWhiteSpace(newLayerName) ? newLayerName : "DefaultLayerName");
      VoroDiagram.OnChanged?.Invoke();
    }

    EditorGUILayout.EndHorizontal();
    DrawUILine(Color.cornflowerBlue);
    LayersGUI();

    serializedObject.ApplyModifiedProperties();
    return;

    void LayersGUI() {
      foreach (var layer in diagram.layers) {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Layer: {layer.layerName}");
        DrawUILine(Color.red);

        NodeGUI(layer.nodes);

        newNodeType = (NodeType)EditorGUILayout.EnumPopup("New Node:", newNodeType);
        if (GUILayout.Button($"Add Node \"{newNodeType.ToString()}\"")) {
          layer.CreateNode(newNodeType);
          VoroDiagram.OnChanged?.Invoke();
        }

        EditorGUILayout.EndVertical();
        DrawUILine(Color.darkOrange);
      }
    }

    void NodeGUI(List<Diagram.Layer.Node> nodes) {
      foreach (var node in nodes) {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Node: {node.type.ToString()}");
        var newMode = (OperationMode)EditorGUILayout.EnumPopup("Mode:", node.mode);
        if (newMode != node.mode) {
          node.mode = newMode;
          VoroDiagram.OnChanged?.Invoke();
        }

        FieldsGUI(node);
        EditorGUILayout.EndVertical();
        DrawUILine(Color.springGreen);
      }
    }

    void FieldsGUI(Diagram.Layer.Node node) {
      foreach (var data in node.data) {
        data.DrawGUI();
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
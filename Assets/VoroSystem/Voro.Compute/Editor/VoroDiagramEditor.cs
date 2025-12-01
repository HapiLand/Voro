using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.Editor {
[CustomEditor(typeof(VoroDiagram))]
public class VoroDiagramEditor : UnityEditor.Editor {
  #region Serialized Fields

  [SerializeField] VoroDiagram voroDiagram;
  [SerializeField] string newLayerName = "";
  [SerializeField] NodeType newNodeType = NodeType.Debug;

  #endregion

  #region Event Functions

  void OnEnable() {
    voroDiagram = target as VoroDiagram;
  }

  public override void OnInspectorGUI() {
    if (!voroDiagram) {
      return;
    }

    serializedObject.Update();

    // GUILayout.Label($"Diagram: {voroDiagram.diagram.diagramName}");

    // Layer creation
    EditorGUILayout.BeginHorizontal();
    newLayerName = EditorGUILayout.TextField("New Layer Name:", newLayerName);
    if (GUILayout.Button($"Create Layer \"{newLayerName}\"")) {
      voroDiagram.CreateLayer(!string.IsNullOrWhiteSpace(newLayerName) ? newLayerName : "DefaultLayer");
    }

    EditorGUILayout.EndHorizontal();

    HelperUtility.DrawUILine(Color.cornflowerBlue);
    // LayersGUI();

    serializedObject.ApplyModifiedProperties();

    /*void LayersGUI() {
      foreach (var layer in voroDiagram.diagram.layers) {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Layer: {layer.layerName}");
        HelperUtility.DrawUILine(Color.red);

        NodeGUI(layer);

        newNodeType = (NodeType)EditorGUILayout.EnumPopup("New Node:", newNodeType);
        if (GUILayout.Button($"Add Node \"{newNodeType}\"")) {
          voroDiagram.CreateNode(layer, newNodeType);
        }

        EditorGUILayout.EndVertical();
        HelperUtility.DrawUILine(Color.darkOrange);
      }
    }*/

    /*void NodeGUI(Diagram.Layer layer) {
      foreach (var node in layer.nodes) {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Node: {node.type}");
        var newMode = (OperationMode)EditorGUILayout.EnumPopup("Mode:", node.mode);
        voroDiagram.SetNodeMode(node, newMode);
        FieldsGUI(node);
        EditorGUILayout.EndVertical();
        HelperUtility.DrawUILine(Color.springGreen);
      }
    }*/

    /*void FieldsGUI(Diagram.Layer.Node node) {
      foreach (var data in node.data) {
        data.DrawGUI();
      }
    }*/

    #endregion
  }
}
}
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Designer.Editor {
[CustomEditor(typeof(VoroDesigner))]
public class DesignerEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
        if (!designer) {
            EditorGUILayout.HelpBox("Designer is null or has been destroyed.", MessageType.Warning);
            return;
        }

        serializedObject.Update();

        if (designer.graph == null) {
            EditorGUILayout.HelpBox("Graph is null.", MessageType.Info);
            serializedObject.ApplyModifiedProperties();
            return;
        }

        GUILayout.Label($"Graph: {designer.graph.graphName}");
        EditorGUILayout.BeginHorizontal();
        newLayerName = EditorGUILayout.TextField("New Layer Name:", newLayerName);
        if (GUILayout.Button($"Create Layer \"{newLayerName}\"")) {
            if (!string.IsNullOrWhiteSpace(newLayerName)) {
                designer.graph.CreateLayer(newLayerName);
                VoroDesigner.OnChanged?.Invoke();
            }
        }

        EditorGUILayout.EndHorizontal();
        DrawUILine(Color.cornflowerBlue);
        LayersGUI();

        serializedObject.ApplyModifiedProperties();
        return;

        void LayersGUI() {
            foreach (var layer in designer.graph.layers) {
                EditorGUILayout.BeginVertical("box");
                GUILayout.Label($"Layer \"{layer.layerName}\"");

                NodeGUI(layer.nodes);

                var names = NodeLookup.Names.ToList();
                var nameStrings = names.Select(n => n.ToString()).ToArray();
                selectedID = EditorGUILayout.Popup("Select Effect", selectedID, nameStrings);
                if (GUILayout.Button("Add Effect")) {
                    var selected = names[selectedID];
                    var def = NodeLookup.Get(selected);
                    layer.CreateNode(def);
                    VoroDesigner.OnChanged?.Invoke();
                }

                EditorGUILayout.EndVertical();
                DrawUILine(Color.darkOrange);
            }
        }

        void NodeGUI(List<Node> nodes) {
            foreach (var node in nodes) {
                EditorGUILayout.BeginVertical("box");
                GUILayout.Label($"Effect \"{node.nodeName}\"");
                var newOp = (EffectOperation)EditorGUILayout.EnumPopup("Operation:", node.operation);
                if (newOp != node.operation) {
                    node.operation = newOp;
                    VoroDesigner.OnChanged?.Invoke(); // Event called here
                }

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

    #region Serialized Fields
    [SerializeReference] VoroDesigner designer;
    [SerializeReference] SerializedProperty graphProp;
    [SerializeReference] string newLayerName = "";
    [SerializeReference] int selectedID;
    #endregion

    #region Event Functions
    void OnEnable() {
        designer = target as VoroDesigner;
        if (designer != null) {
            graphProp = serializedObject.FindProperty("graph");
        }
    }

    void OnDisable() {
        designer = null;
        graphProp = null;
    }
    #endregion
}
}
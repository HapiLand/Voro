using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Compute.Graphs;

namespace VoroSystem.Voro.Compute.Editor {
[CustomEditor(typeof(VoroCompute))]
public class VoroComputeEditor : UnityEditor.Editor {
    #region Serialized Fields
    [SerializeReference] SerializedProperty graphProp;
    [SerializeReference] string newLayerName = "";
    [SerializeReference] int selectedID;
    [SerializeReference] VoroCompute compute;
    #endregion
    public override void OnInspectorGUI() {
        if (!compute) {
            return;
        }

        serializedObject.Update();
        DrawUILine(Color.black);
        if (GUILayout.Button("Compute")) {
            VoroCompute.OnCompute?.Invoke();
        }
        DrawUILine(Color.black);

        GUILayout.Label($"Graph: {compute.graph.graphName}");
        EditorGUILayout.BeginHorizontal();
        newLayerName = EditorGUILayout.TextField("New Layer Name:", newLayerName);
        if (GUILayout.Button($"Create Layer \"{newLayerName}\"")) {
            if (!string.IsNullOrWhiteSpace(newLayerName)) {
                compute.graph.CreateLayer(newLayerName);
                VoroCompute.OnChanged?.Invoke();
            }
        }

        EditorGUILayout.EndHorizontal();
        DrawUILine(Color.cornflowerBlue);
        LayersGUI();

        serializedObject.ApplyModifiedProperties();
        return;

        void LayersGUI() {
            foreach (var layer in compute.graph.layers) {
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
                    VoroCompute.OnChanged?.Invoke();
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
                    VoroCompute.OnChanged?.Invoke(); // Event called here
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

    #region Event Functions
    void OnEnable() {
        compute = target as VoroCompute;
        if (compute != null) {
            graphProp = serializedObject.FindProperty("graph");
        }
    }

    void OnDisable() {
        compute = null;
        graphProp = null;
        
    }
    #endregion
}
}
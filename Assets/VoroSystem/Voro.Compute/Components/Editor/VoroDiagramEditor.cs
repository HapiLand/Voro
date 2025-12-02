using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.Components.Editor {
[CustomEditor(typeof(VoroDiagram))]
public class VoroDiagramEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
        if (!voroDiagram) {
            return;
        }

        serializedObject.Update();
        DrawUILine(Color.black);
        if (GUILayout.Button("Compute")) {
            VoroCompute.OnCompute?.Invoke();
        }

        DrawUILine(Color.black);

        GUILayout.Label($"Diagram: {voroDiagram.diagram.diagramName}");
        EditorGUILayout.BeginHorizontal();
        newLayerName = EditorGUILayout.TextField("New Layer Name:", newLayerName);
        if (GUILayout.Button($"Create Layer \"{newLayerName}\"")) {
            if (!string.IsNullOrWhiteSpace(newLayerName)) {
                voroDiagram.diagram.CreateLayer(newLayerName);
                VoroCompute.OnChanged?.Invoke();
            }
        }

        EditorGUILayout.EndHorizontal();
        DrawUILine(Color.cornflowerBlue);
        LayersGUI();

        serializedObject.ApplyModifiedProperties();
        return;

        void LayersGUI() {
            foreach (var layer in voroDiagram.diagram.layers) {
                EditorGUILayout.BeginVertical("box");
                GUILayout.Label($"Layer \"{layer.layerName}\"");

                NodeGUI(layer.nodes);

                var names = Enum.GetValues(typeof(EffectBase.EffectType)).Cast<EffectBase.EffectType>().ToList();
                var nameStrings = names.Select(n => n.ToString()).ToArray();
                selectedID = EditorGUILayout.Popup("Select Effect", selectedID, nameStrings);
                if (GUILayout.Button("Add Effect")) {
                    var selected = names[selectedID];
                    var def = NodeFactory.Create(selected);
                    layer.CreateNode(def);
                    VoroCompute.OnChanged?.Invoke();
                }

                EditorGUILayout.EndVertical();
                DrawUILine(Color.darkOrange);
            }
        }

        void NodeGUI(List<INode> nodes) {
            foreach (var node in nodes) {
                EditorGUILayout.BeginVertical("box");
                GUILayout.Label($"Effect \"{node.NodeType}\"");
                var newOp = (EffectBase.EffectMode)EditorGUILayout.EnumPopup("Operation:", node.Mode);
                if (newOp != node.Mode) {
                    node.Mode = newOp;
                    VoroCompute.OnChanged?.Invoke(); // Event called here
                }

                FieldsGUI(node);
                EditorGUILayout.EndVertical();
                DrawUILine(Color.springGreen);
            }
        }

        void FieldsGUI(INode node) {
            foreach (var field in node.Fields) {
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
    [SerializeReference] SerializedProperty graphProp;
    [SerializeReference] string newLayerName = "";
    [SerializeReference] int selectedID;
    [SerializeReference] VoroDiagram voroDiagram;
    #endregion

    #region Event Functions
    void OnEnable() {
        voroDiagram = target as VoroDiagram;
        if (voroDiagram != null) {
            graphProp = serializedObject.FindProperty("diagram");
        }
    }

    void OnDisable() {
        voroDiagram = null;
        graphProp = null;
    }
    #endregion
}
}
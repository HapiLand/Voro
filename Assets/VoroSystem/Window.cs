using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VoroSystem {
class Window : EditorWindow {
    void OnGUI() {
        var editor = VoroEditor.Instance;

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Layers", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        for (var i = 0; i < editor.LayerContent.Count; i++) {
            var layer = editor.LayerContent[i];

            if (!DrawLayer(layer, i)) {
                i--; // reduce if the layer is removed from the list
            }
        }

        EditorGUI.indentLevel--;

        if (GUILayout.Button("+ Layer +")) {
            Debug.Log("Add Layer");
        }

        EditorGUILayout.EndVertical();

        return;

        bool DrawLayer(LayerData layer, int layerIndex) {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(layer.Name, layer.Active ? EditorStyles.boldLabel : EditorStyles.label);
            layer.Active = EditorGUILayout.Toggle("", layer.Active);

            if (GUILayout.Button("↑")) {
                layer.MoveUp();
            /*if (GUILayout.Button("↑") && layerIndex > 0) {
                var temp = editor.LayerContent[layerIndex - 1];
                editor.LayerContent[layerIndex - 1] = layer;
                editor.LayerContent[layerIndex] = temp;
            }*/
            }
            if (GUILayout.Button("↓")) {
                layer.MoveDown();
            /*if (GUILayout.Button("↓") && layerIndex < editor.LayerContent.Count - 1) {
                var temp = editor.LayerContent[layerIndex + 1];
                editor.LayerContent[layerIndex + 1] = layer;
                editor.LayerContent[layerIndex] = temp;
            }*/
            }

            if (GUILayout.Button("🗑")) {
                editor.LayerContent.RemoveAt(layerIndex);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                return false; // layer removed
            }

            EditorGUILayout.EndHorizontal();

            if (layer.Active) {
                DrawNodes(layer.Content.ToList());

                if (GUILayout.Button("+ Node +")) {
                    Debug.Log("Add Node");
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            return true;
        }

        void DrawNodes(List<Node> nodes) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Nodes");
            EditorGUI.indentLevel++;

            for (var i = 0; i < nodes.Count; i++) {
                var fx = nodes[i];
                if (!DrawNode(fx, nodes, i)) {
                    i--; // adjust index if node was removed
                }
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        bool DrawNode(Node node, List<Node> nodes, int nodeIndex) {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(node.Name, node.Active ? EditorStyles.boldLabel : EditorStyles.label);
            node.Active = EditorGUILayout.Toggle("", node.Active);

            if (GUILayout.Button("↑")) {
                node.MoveUp();
                /*if (GUILayout.Button("↑") && nodeIndex > 0) {
                       var temp = nodes[nodeIndex - 1];
                       nodes[nodeIndex - 1] = fx;
                       nodes[nodeIndex] = temp;
                   }*/
            }
            if (GUILayout.Button("↓")) {
                node.MoveDown();
                /*if (GUILayout.Button("↓") && nodeIndex < nodes.Count - 1) {
                       var temp = nodes[nodeIndex + 1];
                       nodes[nodeIndex + 1] = fx;
                       nodes[nodeIndex] = temp;
                   }*/
            }
            
            

            if (GUILayout.Button("🗑")) {
                nodes.RemoveAt(nodeIndex);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                return false; // node removed
            }

            EditorGUILayout.EndHorizontal();

            if (node.Active) {
                DrawNodeControls(node.Controls);
            }

            EditorGUILayout.EndVertical();
            return true; // node still exists
        }

        void DrawNodeControls(Control[] controls) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Controls");
            EditorGUI.indentLevel++;

            foreach (var control in controls) {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField(control.Name);
                control.Value = EditorGUILayout.FloatField(control.Value);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }
    }

    public static void ShowWindow() {
        var wnd = GetWindow<Window>();
        wnd.titleContent = new GUIContent("Editor Window");
    }
}
}
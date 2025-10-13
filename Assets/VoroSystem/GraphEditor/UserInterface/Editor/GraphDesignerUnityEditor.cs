using UnityEditor;
using UnityEngine;
using VoroSystem.GraphEditor.UserInterface.Elements;

namespace VoroSystem.GraphEditor.UserInterface.Editor {
[CustomEditor(typeof(GraphDesigner))]
public class DesignerUnityEditor : UnityEditor.Editor {
    IDesigner _designer;
    string _newLayerName = "SomeLayerNameGoesHere";

    void OnEnable() {
        _designer = (IDesigner)target;
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Graph Designer");

        EditorGUI.indentLevel++;
        // draw each Layer, which draws each Node
        _designer.ForEach(layer => {
            EditorGUILayout.BeginHorizontal();
            layer.Draw();
            if (GUILayout.Button("↑")) {
                _designer.MoveUp(layer);
            }

            if (GUILayout.Button("↓")) {
                _designer.MoveDown(layer);
            }

            if (GUILayout.Button("🗑")) {
                _designer.Remove(layer);
            }

            EditorGUILayout.EndHorizontal();
        });
        EditorGUI.indentLevel--;


        // create new Layer
        _newLayerName = EditorGUILayout.TextField("Layer Name", _newLayerName);
        if (GUILayout.Button("Create Layer")) {
            if (string.IsNullOrWhiteSpace(_newLayerName)) {
                EditorUtility.DisplayDialog("Error", "Layer name not set", "OK");
                return;
            }

            _designer.Add(Layer.CreateInstance(_newLayerName));
        }

        EditorGUILayout.EndVertical();
    }
}
}
using UnityEditor;
using UnityEngine;

namespace VoroSystem.Editor {
[CustomEditor(typeof(WorldController))]
public class WorldControllerEditor : UnityEditor.Editor {
    WorldController _controller;

    void OnEnable() {
        _controller = (WorldController)target;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        if (GUILayout.Button("Generate World map")) {
            _controller.GenerateWorldMap();
        }

        if (GUILayout.Button("Launch Editor")) {
            _controller.LaunchEditor();
        }

        // DrawLayerNodePairs();

        serializedObject.ApplyModifiedProperties();
    }

    void DrawLayerNodePairs() {
        var listPairs = _controller.Contents;

        if (GUILayout.Button("Add Layer")) {
            listPairs.Add(new LayerNodePair($"Layer:{Random.Range(0, 9999)}"));
        }

        for (var i = 0; i < listPairs.Count; i++) {
            var pair = listPairs[i];

            EditorGUILayout.BeginVertical("box");

            // heading row
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(pair.Layer, EditorStyles.boldLabel);
            if (GUILayout.Button($"Remove Layer {pair.Layer}")) {
                listPairs.RemoveAt(i);
                i--;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                continue;
            }

            EditorGUILayout.EndHorizontal();

            // list section
            pair.FoldoutState = EditorGUILayout.Foldout(pair.FoldoutState, "Nodes:");
            if (pair.FoldoutState) {
                EditorGUI.indentLevel++;
                var values = pair.Nodes;

                for (var j = 0; j < values.Count; j++) {
                    var node = values[j];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(node);
                    if (GUILayout.Button("Remove Node")) {
                        values.RemoveAt(j);
                        j--;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Node")) {
                    values.Add($"Node:{Random.Range(0, 9999)}");
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
    }
}
}
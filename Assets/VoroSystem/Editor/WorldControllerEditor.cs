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

        if (GUILayout.Button("Run LifeCycle Once")) {
            _controller.RunLifeCycleOnce();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
}
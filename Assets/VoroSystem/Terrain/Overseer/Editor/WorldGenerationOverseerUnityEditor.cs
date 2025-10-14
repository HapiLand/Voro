using UnityEditor;
using UnityEngine;

namespace VoroSystem.Terrain.Overseer.Editor {
[CustomEditor(typeof(WorldGenerationOverseer))]
public class WorldGenerationOverseerUnityEditor : UnityEditor.Editor {
    IWorldGenerationOverseer _target;

    void OnEnable() {
        _target = (IWorldGenerationOverseer)target;
    }

    public override void OnInspectorGUI() {
        /*EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("World Generation Overseer");

        if (GUILayout.Button("Generate World")) {
            _target.GenerateWorld();
        }

        if (GUILayout.Button("Generate Single Tile")) { }

        EditorGUILayout.EndVertical();*/
    }
}
}
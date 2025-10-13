using UnityEditor;
using UnityEngine;

namespace VoroSystem.WorldGrid.Editor {
[CustomEditor(typeof(WorldMap))]
public class WorldMapUnityEditor : UnityEditor.Editor {
    IWorld _world;

    void OnEnable() {
        _world = (IWorld)target;
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("2D World Map");

        EditorGUILayout.BeginHorizontal();
        WorldMapArray(); // set dimensions of the world map
        EditorGUILayout.EndHorizontal();


        if (_world.HasMap) {
            // only allow instancing when the map exists
            EditorGUILayout.BeginHorizontal();
            WorldMapInstancing();
            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(10f);
        return;

        void WorldMapArray() {
            // dimensions of world map
            EditorGUILayout.LabelField("Dimensions");

            var mapSize = _world.Size;
            mapSize.x = EditorGUILayout.IntField(mapSize.x);
            mapSize.y = EditorGUILayout.IntField(mapSize.y);
            _world.SetMapSize(mapSize.x, mapSize.y); // update the size

            EditorGUILayout.Space();
            if (GUILayout.Button("Generate Array")) {
                if (_world is WorldMap w) {
                    w.GenerateMapArray();
                }
            }
        }

        void WorldMapInstancing() {
            // generate GameObjects
            EditorGUILayout.LabelField($"World Map '{_world.Size.x}x{_world.Size.y}'");
            if (GUILayout.Button("Instantiate Map")) {
                _world.InstantiateMap();
            }
        }
    }
}
}
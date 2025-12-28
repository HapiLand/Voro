using UnityEditor;
using UnityEngine;
using Voro.Internal.World.PlayerOrigins;

namespace Voro.Editor {
public class PlayerSpawnerWindow : EditorWindow {
  #region Serialized Fields
  [SerializeField] Vector3 spawnPosition = Vector3.zero;
  #endregion

  #region Event Functions
  void OnGUI() {
    EditorGUILayout.LabelField("Enter Position:", EditorStyles.boldLabel);

    spawnPosition.x = EditorGUILayout.FloatField("X", spawnPosition.x);
    spawnPosition.y = EditorGUILayout.FloatField("Y", spawnPosition.y);
    spawnPosition.z = EditorGUILayout.FloatField("Z", spawnPosition.z);

    GUILayout.Space(10);

    if (GUILayout.Button("Spawn")) {
      SpawnPlayer(spawnPosition);
      Close();
    }
  }
  #endregion

  [MenuItem("Voro/Spawn Player")]
  public static void ShowWindow() {
    var window = GetWindow<PlayerSpawnerWindow>("Spawn Player");
    window.minSize = new Vector2(250, 100);
  }

  static void SpawnPlayer(Vector3 position) {
    var obj = new GameObject("Player")
    {
      transform =
      {
        position = position
      }
    };
    var player = obj.AddComponent<PlayerOrigin>();
    Selection.activeGameObject = obj;
  }
}
}
using UnityEditor;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.Editor {
public class WorldGenEditorWindow : EditorWindow {
  WorldGenerator generator;
  int mapHeight;
  int mapWidth;

  #region Event Functions
  void OnEnable() {
    mapWidth = WorldGenMapSettings.Width;
    mapHeight = WorldGenMapSettings.Height;
  }

  void OnGUI() {
    if (generator == null) {
      InitWorldGenerator();
    }

    EditorGUILayout.LabelField("World Generator", EditorStyles.boldLabel);
    EditorGUILayout.Space();

    EditorGUILayout.LabelField("State:", generator.stateMachine.currentState.ToString());

    DrawMapSettings();
    EditorGUILayout.Space();

    DrawGenerationButtons();

    Repaint();
  }
  #endregion

  void DrawMapSettings() {
    EditorGUILayout.LabelField("Map Settings", EditorStyles.boldLabel);
    mapWidth = EditorGUILayout.IntField("Width", mapWidth);
    mapHeight = EditorGUILayout.IntField("Height", mapHeight);
    if (GUILayout.Button("Apply Map Settings")) {
      WorldGenMapSettings.SetDimensions(mapWidth, mapHeight);
    }
  }

  void DrawGenerationButtons() {
    if (generator == null || generator.stateMachine == null) {
      GUI.enabled = false;
      EditorGUILayout.LabelField("WorldGenerator not found");
      GUI.enabled = true;
      return;
    }

    var state = generator.stateMachine.currentState;

    // Start Generation Button
    GUI.enabled = state is WorldGenState.GenerationState.NotCreated or WorldGenState.GenerationState.GenerationComplete;
    if (GUILayout.Button("Start Generation")) {
      generator.StartGeneration();
    }

    // Destroy Generator Button
    GUI.enabled = generator != null;
    if (GUILayout.Button("Clear World")) {
      DestroyGenerator();
    }

    GUI.enabled = generator != null;
    if (GUILayout.Button("Terminate")) {
      DestroyGenerator();
      Close();
    }

    GUI.enabled = true;
  }

  void InitWorldGenerator() {
    generator = FindObjectOfType<WorldGenerator>();
    if (generator) {
      return;
    }

    var go = new GameObject("WorldGenerator");
    generator = go.AddComponent<WorldGenerator>();

    Debug.Log("WorldGenerator created");
  }

  [MenuItem("VoroNew/World")]
  public static void ShowWindow() {
    var wnd = GetWindow<WorldGenEditorWindow>();
    wnd.titleContent = new GUIContent("World Generation");
    wnd.InitWorldGenerator();
  }

  void DestroyGenerator() {
    if (!generator) {
      return;
    }

    DestroyImmediate(generator.gameObject);
    generator = null;
    Debug.Log("WorldGenerator destroyed");
  }
}
}
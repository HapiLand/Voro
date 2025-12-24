using UnityEditor;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.Editor {
public class WorldGenEditorWindow : EditorWindow {
  WorldGenerator _generator;
  void OnEnable() {
    WorldGenEditorEvents.OnParametersChanged += OnParametersChanged;
  }
  void OnDisable() {
    WorldGenEditorEvents.OnParametersChanged -= OnParametersChanged;
  }
  void OnGUI() {
    if (_generator == null) {
      InitWorldGenerator();
    }

    EditorGUILayout.LabelField("World Generator", EditorStyles.boldLabel);
    EditorGUILayout.Space();

    EditorGUILayout.LabelField("State:", _generator.GetCurrentState().ToString());

    EditorGUILayout.Space();

    DrawGenerationButtons();
    Repaint();
  }
  void OnParametersChanged() {
    if (_generator == null) {
      return;
    }
    DestroyGenerator();
    InitWorldGenerator();
    _generator.StartGeneration();
    Repaint();
  }
  void DrawGenerationButtons() {
    if (_generator == null || _generator.stateMachine == null) {
      EditorGUILayout.HelpBox("WorldGenerator not found", MessageType.Warning);
      return;
    }
    var state = _generator.GetCurrentState();

    // start generator button
    GUI.enabled = state is WorldGenState.GenerationState.NotCreated or WorldGenState.GenerationState.GenerationComplete;
    if (GUILayout.Button("Start Generation")) {
      _generator.StartGeneration();
    }

    // destroy generator button
    GUI.enabled = _generator != null;
    if (GUILayout.Button("Clear World")) {
      DestroyGenerator();
    }

    // terminate button
    GUI.enabled = _generator != null;
    if (GUILayout.Button("Terminate")) {
      DestroyGenerator();
      Close();
    }

    GUI.enabled = true;
  }

  void InitWorldGenerator() {
    _generator = FindFirstObjectByType<WorldGenerator>();
    if (_generator) {
      return;
    }
    // todo helper utility to create a game object
    var go = new GameObject("WorldGenerator");
    _generator = go.AddComponent<WorldGenerator>();
    Debug.Log("WorldGenerator created");
  }

  [MenuItem("Voro/World Generator")]
  public static void ShowWindow() {
    var wnd = GetWindow<WorldGenEditorWindow>();
    wnd.titleContent = new GUIContent("World Generation");
    wnd.InitWorldGenerator();
  }

  void DestroyGenerator() {
    if (!_generator) {
      return;
    }
    DestroyImmediate(_generator.gameObject);
    _generator = null;
    Debug.Log("WorldGenerator destroyed");
  }
}
}
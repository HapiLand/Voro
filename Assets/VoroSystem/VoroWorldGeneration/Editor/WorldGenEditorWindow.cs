using UnityEditor;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Editor {
public class WorldGenEditorWindow : EditorWindow {
  WorldGenerator _worldGenerator;

  #region Event Functions
  void OnEnable() {
    WorldGenEditorEvents.OnParametersChanged += OnParametersChanged;
  }

  void OnDisable() {
    WorldGenEditorEvents.OnParametersChanged -= OnParametersChanged;
  }

  void OnGUI() {
    InitWorldGenerator();

    EditorGUILayout.LabelField("World Generator", EditorStyles.boldLabel);
    EditorGUILayout.Space();

    if (_worldGenerator == null || _worldGenerator.CubeWorld == null) {
      EditorGUILayout.HelpBox("WorldGenerator not found", MessageType.Warning);
      return;
    }

    DrawStateInfo();
    EditorGUILayout.Space();
    DrawGenerationButtons();

    Repaint();
  }
  #endregion

  void DrawStateInfo() {
    var state = _worldGenerator.CubeWorld.worldState;

    if (state == null) {
      EditorGUILayout.HelpBox("WorldState missing", MessageType.Warning);
      return;
    }

    EditorGUILayout.LabelField("Current State:", state.CurrentStateName);
  }

  void OnParametersChanged() {
    if (_worldGenerator == null) {
      return;
    }

    DestroyGenerator();
    InitWorldGenerator();
    _worldGenerator.StartGeneration();
    Repaint();
  }

  void DrawGenerationButtons() {
    var worldState = _worldGenerator.CubeWorld.worldState;
    GUI.enabled = worldState != null;
    if (GUILayout.Button("Start Generation")) {
      _worldGenerator.StartGeneration();
    }

    GUI.enabled = _worldGenerator != null;
    if (GUILayout.Button("Clear World")) {
      DestroyGenerator();
    }

    GUI.enabled = _worldGenerator != null;
    if (GUILayout.Button("Terminate")) {
      DestroyGenerator();
      Close();
    }

    GUI.enabled = true;
  }

  void InitWorldGenerator() {
    if (_worldGenerator != null) {
      return;
    }

    _worldGenerator = FindFirstObjectByType<WorldGenerator>();

    if (_worldGenerator != null) {
      return;
    }

    var go = new GameObject("WorldGenerator");
    _worldGenerator = go.AddComponent<WorldGenerator>();
  }

  [MenuItem("Voro/World Generator")]
  public static void ShowWindow() {
    var wnd = GetWindow<WorldGenEditorWindow>();
    wnd.titleContent = new GUIContent("World Generator");
    wnd.InitWorldGenerator();
  }

  void DestroyGenerator() {
    if (!_worldGenerator) {
      return;
    }

    DestroyImmediate(_worldGenerator.gameObject);
    _worldGenerator = null;
    Debug.Log("WorldGenerator destroyed");
  }
}
}
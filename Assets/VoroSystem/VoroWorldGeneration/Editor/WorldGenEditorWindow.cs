using System;
using UnityEditor;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.Editor {
public class WorldGenEditorWindow : EditorWindow {
  WorldGenerator _generator;
  int _mapHeight;
  int _mapWidth;
  bool _autoRefreshEnabled = false;
  double _nextRefreshTime = 0f;
  const double RefreshInterval = 5.0;

  #region Event Functions
  void OnEnable() {
    _mapWidth = WorldGenMapSettings.Width;
    _mapHeight = WorldGenMapSettings.Height;
    EditorApplication.update += AutoRefreshUpdate;
    WorldGenEditorEvents.OnParametersChanged += OnParametersChanged;
  }

  void OnDisable() {
    EditorApplication.update -= AutoRefreshUpdate;
    WorldGenEditorEvents.OnParametersChanged -= OnParametersChanged;
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
  void AutoRefreshUpdate() {
    if (!_autoRefreshEnabled || _generator == null) {
      return;
    }

    if (EditorApplication.timeSinceStartup >= _nextRefreshTime) {
      _nextRefreshTime = EditorApplication.timeSinceStartup + RefreshInterval;

      // Simulate Clear World + Start Generation
      DestroyGenerator();
      InitWorldGenerator();
      _generator.StartGeneration();

      Repaint();
    }
  }
  void OnGUI() {
    if (_generator == null) {
      InitWorldGenerator();
    }

    EditorGUILayout.LabelField("World Generator", EditorStyles.boldLabel);
    EditorGUILayout.Space();

    EditorGUILayout.LabelField("State:", _generator.stateMachine.currentState.ToString());

    DrawMapSettings();
    EditorGUILayout.Space();

    DrawGenerationButtons();

    EditorGUILayout.Space();
    _autoRefreshEnabled = EditorGUILayout.Toggle("Auto Refresh", _autoRefreshEnabled);
    _nextRefreshTime = _autoRefreshEnabled switch
    {
      true when _nextRefreshTime == 0f => EditorApplication.timeSinceStartup + RefreshInterval,
      false => 0f,
      _ => _nextRefreshTime
    };

    Repaint();
  }
  #endregion

  void DrawMapSettings() {
    EditorGUILayout.LabelField("Map Settings", EditorStyles.boldLabel);
    _mapWidth = EditorGUILayout.IntField("Width", _mapWidth);
    _mapHeight = EditorGUILayout.IntField("Height", _mapHeight);
    if (GUILayout.Button("Apply Map Settings")) {
      WorldGenMapSettings.SetDimensions(_mapWidth, _mapHeight);
    }
  }

  void DrawGenerationButtons() {
    if (_generator == null || _generator.stateMachine == null) {
      GUI.enabled = false;
      EditorGUILayout.LabelField("WorldGenerator not found");
      GUI.enabled = true;
      return;
    }

    var state = _generator.stateMachine.currentState;

    // Start Generation Button
    GUI.enabled = state is WorldGenState.GenerationState.NotCreated or WorldGenState.GenerationState.GenerationComplete;
    if (GUILayout.Button("Start Generation")) {
      _generator.StartGeneration();
    }

    // Destroy Generator Button
    GUI.enabled = _generator != null;
    if (GUILayout.Button("Clear World")) {
      DestroyGenerator();
    }

    GUI.enabled = _generator != null;
    if (GUILayout.Button("Terminate")) {
      DestroyGenerator();
      Close();
    }

    GUI.enabled = true;
  }

  void InitWorldGenerator() {
    _generator = FindObjectOfType<WorldGenerator>();
    if (_generator) {
      return;
    }

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
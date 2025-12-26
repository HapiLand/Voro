using UnityEditor;
using UnityEngine;

namespace Voro.UserInterface.Internal.TerrainComputes.Editor {
public class ComputeFactoryWindow : EditorWindow {
  ComputeData _newComputeData;
  Vector2 _scrollPos;
  bool _showNew;

  #region Event Functions
  void OnGUI() {
    GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.ExpandWidth(false));
    DrawComputeList();
    DrawCreateBox();
    GUILayout.EndHorizontal();
  }
  #endregion

  void DrawComputeList() {
    GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));

    var list = ComputeAssetUtility.GetAssetList();
    if (list.Count == 0) {
      EditorGUILayout.LabelField("No Computes");
      GUILayout.EndVertical();
      return;
    }

    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(250));
    EditorGUILayout.LabelField("Computes:", EditorStyles.boldLabel);
    foreach (var compute in list) {
      EditorGUILayout.LabelField(compute.kernel);
    }

    EditorGUILayout.EndScrollView();

    GUILayout.EndVertical();
  }

  void DrawCreateBox() {
    GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(true));

    if (!_showNew) {
      if (GUILayout.Button("New Compute")) {
        _showNew = true;
        _newComputeData = new ComputeData
        {
          InputKernel = "Default Kernel"
        };
      }
    }

    if (!_showNew || _newComputeData == null) {
      GUILayout.EndVertical();
      return;
    }

    _newComputeData.InputKernel = EditorGUILayout.TextField("Kernel:", _newComputeData.InputKernel);
    GUILayout.Space(20f);
    GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.ExpandWidth(false));


    if (GUILayout.Button("Create Compute")) {
      if (!string.IsNullOrEmpty(_newComputeData.InputKernel)) {
        var shouldCreate = true;

        if (ComputeAssetUtility.DoesAssetExist(_newComputeData.InputKernel)) {
          shouldCreate = EditorUtility.DisplayDialog(
            $"Compute {_newComputeData.InputKernel} already exists",
            "Overwrite Compute?",
            "Overwrite",
            "Cancel"
          );

          if (shouldCreate) {
            AssetDatabase.DeleteAsset(ComputeAssetUtility.GetAssetPath(_newComputeData.InputKernel));
          }
        }

        if (shouldCreate) {
          _newComputeData.ApplyInputs();
          ComputeFactory.CreateCompute(_newComputeData);
          _newComputeData = null;
          _showNew = false;
        }
      }
    }


    if (_showNew) {
      if (GUILayout.Button("Cancel")) {
        var confirmCancel = EditorUtility.DisplayDialog(
          "Cancel",
          "Cancel Creation?",
          "Yes",
          "No"
        );
        if (confirmCancel) {
          _newComputeData = null;
          _showNew = false;
        }
      }
    }

    GUILayout.EndHorizontal();


    GUILayout.EndVertical();
  }

  [MenuItem("Voro/UI/Terrain Compute Factory")]
  public static void OpenWindow() {
    var wnd = GetWindow<ComputeFactoryWindow>();
    wnd.titleContent = new GUIContent("Terrain Compute Factory");
  }
}
}
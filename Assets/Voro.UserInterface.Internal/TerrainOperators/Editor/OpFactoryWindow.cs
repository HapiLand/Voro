using UnityEditor;
using UnityEngine;
using Voro.UserInterface.Internal.TerrainComputes;
using Voro.UserInterface.Internal.TerrainComputes.Editor;

namespace Voro.UserInterface.Internal.TerrainOperators.Editor {
public class OpFactoryWindow : EditorWindow {
  OpData _newOpData;
  Vector2 _scrollPos;
  bool _showNew;
  int _selectedComputeIndex = -1;
  Vector2 _scrollComputePos;

  #region Event Functions
  void OnGUI() {
    if (ComputeAssetUtility.GetAssetList().Count == 0) {
      EditorGUILayout.LabelField("No Computes");
      return;
    }
    GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.ExpandWidth(false));
    DrawOpList();
    DrawCreateBox();
    GUILayout.EndHorizontal();
  }
  #endregion

  void DrawOpList() {
    GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));

    var list = OpAssetUtility.GetAssetList();
    if (list.Count == 0) {
      EditorGUILayout.LabelField("No Operators");
      GUILayout.EndVertical();
      return;
    }

    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(250));
    EditorGUILayout.LabelField("Operators:", EditorStyles.boldLabel);
    foreach (var op in list) {
      EditorGUILayout.LabelField(op.title);
    }

    EditorGUILayout.EndScrollView();

    GUILayout.EndVertical();
  }

  void DrawCreateBox() {
    GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(true));

    if (!_showNew) {
      if (GUILayout.Button("New Operator")) {
        _showNew = true;
        _newOpData = new OpData
        {
          InputTitle = "Default Title"
        };
      }
    }

    if (!_showNew || _newOpData == null) {
      GUILayout.EndVertical();
      return;
    }
    


    TitleField();
    DrawComputeList();

    GUILayout.Space(20f);
    GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.ExpandWidth(false));
    
    if (GUILayout.Button("Create Operator")) {
      if (!string.IsNullOrEmpty(_newOpData.InputTitle)) {
        var shouldCreate = true;

        if (OpAssetUtility.DoesAssetExist(_newOpData.InputTitle)) {
          shouldCreate = EditorUtility.DisplayDialog(
            $"Operator {_newOpData.InputTitle} already exists",
            "Overwrite Operator?",
            "Overwrite",
            "Cancel"
          );

          if (shouldCreate) {
            AssetDatabase.DeleteAsset(OpAssetUtility.GetAssetPath(_newOpData.InputTitle));
          }
        }
        
        var computeList = ComputeAssetUtility.GetAssetList();
        if (_selectedComputeIndex >= 0 && _selectedComputeIndex < computeList.Count) {
          _newOpData.InputCompute = computeList[_selectedComputeIndex];
        }
        
        if (shouldCreate) {
          _newOpData.ApplyInputs();
          OpFactory.CreateOperator(_newOpData);
          _newOpData = null;
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
          _newOpData = null;
          _showNew = false;
        }
      }
    }

    GUILayout.EndHorizontal();


    GUILayout.EndVertical();
  }

  void DrawComputeList() {
    GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
    var list = ComputeAssetUtility.GetAssetList();
    if (list.Count == 0) {
      EditorGUILayout.LabelField("No Computes");
      GUILayout.EndVertical();
      return;
    }
    _scrollComputePos = EditorGUILayout.BeginScrollView(_scrollComputePos, GUILayout.Width(250));
    EditorGUILayout.LabelField("Computes:", EditorStyles.boldLabel);
    
    var kernelNames = list.ConvertAll(c => c.kernel).ToArray();
    
    _selectedComputeIndex = EditorGUILayout.Popup("Select Compute:", _selectedComputeIndex, kernelNames);

    EditorGUILayout.EndScrollView();
    GUILayout.EndVertical();
  }

  void TitleField() {
    _newOpData.InputTitle = EditorGUILayout.TextField("Title:", _newOpData.InputTitle);
  }

  [MenuItem("Voro/UI/Terrain Operator Factory")]
  public static void OpenWindow() {
    var wnd = GetWindow<OpFactoryWindow>();
    wnd.titleContent = new GUIContent("Terrain Operator Factory");
  }
}
}
using UnityEditor;
using UnityEngine;
using Voro.Internal;

namespace Voro.Wizard.Editor {
[CustomEditor(typeof(Wizard))]
[InitializeOnLoad]
public class WizardEditor : UnityEditor.Editor {
  #region Serialized Fields
  [SerializeField] GUIStyle titleStyle;
  [SerializeField] GUIStyle headingStyle;
  [SerializeField] GUIStyle bodyStyle;
  [SerializeField] GUIStyle buttonStyle;
  #endregion

  static WizardEditor() {
    EditorApplication.delayCall += SelectWizardAutomatically;
  }

  [MenuItem("Voro/Debug/Simulate Package Install")]
  static void FakeFirstTimeInstall() {
    // reset session states
    WizardSessionStates.SetShowedState(false);

    // remove persistence directory
    AssetUtility.RemoveDirectory(WizardAssetPaths.WizardPath);
    AssetUtility.RemoveDirectory($"{WizardAssetPaths.WizardPath}.meta");

    // domain reload
    EditorUtility.RequestScriptReload();
  }

  /// <summary>
  /// Opens the Wizard upon the package being loaded the first time
  /// </summary>
  static void SelectWizardAutomatically() {
    var showed = WizardSessionStates.GetShowedState();
    if (showed) {
      Debug.LogWarning("Wizard already showed");
      return;
    }

    WizardAssetUtility.CreateAndSelectWizard();
    WizardSessionStates.SetShowedState(true);
  }

  protected override void OnHeaderGUI() {
    var wizard = (Wizard)target;
    InitStyles();
    GUILayout.BeginHorizontal();
    {
      GUILayout.BeginVertical();
      {
        GUILayout.FlexibleSpace();
        GUILayout.Label(wizard.title, titleStyle);
        GUILayout.FlexibleSpace();
      }
      GUILayout.EndVertical();
      GUILayout.FlexibleSpace();
    }
    GUILayout.EndHorizontal();
  }

  void InitStyles() {
    bodyStyle = new GUIStyle(EditorStyles.label)
    {
      wordWrap = true,
      fontSize = 14,
      richText = true
    };

    titleStyle = new GUIStyle(bodyStyle)
    {
      fontSize = 26
    };

    headingStyle = new GUIStyle(bodyStyle)
    {
      fontStyle = FontStyle.Bold,
      fontSize = 18
    };

    buttonStyle = new GUIStyle(EditorStyles.miniButton)
    {
      fontStyle = FontStyle.Bold
    };
  }


  public override void OnInspectorGUI() {
    var wizard = (Wizard)target;
    InitStyles();

    foreach (var section in wizard.sections) {
      if (!string.IsNullOrEmpty(wizard.title)) {
        GUILayout.Label(section.heading, headingStyle);
      }

      if (!string.IsNullOrEmpty(section.text)) {
        GUILayout.Label(section.text, bodyStyle);
      }

      GUILayout.Space(10);
    }


    if (GUILayout.Button("Disable Wizard", buttonStyle)) {
      DisableWizard();
    }
  }

  static void DisableWizard() {
    if (AssetUtility.DoesDirectoryExist(WizardAssetPaths.WizardLockPath)) {
      return;
    }

    if (!EditorUtility.DisplayDialog("Disable Wizard", "Proceed?", "Proceed", "Cancel")) {
      return;
    }

    AssetUtility.CreateDirectory(WizardAssetPaths.WizardLockPath);
    AssetUtility.RemoveAsset<Wizard>(WizardAssetPaths.WizardPath);
    AssetDatabase.Refresh();
    Selection.activeObject = null;
  }
}
}
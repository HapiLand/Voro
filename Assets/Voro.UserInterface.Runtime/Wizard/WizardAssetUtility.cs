using System.IO;
using UnityEditor;
using UnityEngine;

namespace Voro.UserInterface.Runtime.Wizard {
public static class WizardAssetUtility {
  public static void SelectWizard() {
    GetOrCreateAsset(out var wizard);
    Selection.objects = new Object[] { wizard };
  }

  static void GetOrCreateAsset(out Wizard wizard) {
    wizard = AssetDatabase.LoadAssetAtPath<Wizard>(WizardAssetPaths.WizardAssetPath);
    if (wizard) {
      return;
    }

    Debug.Log("Wizard asset not found, creating");
    CreateAsset(out wizard);
  }

  static void CreateAsset(out Wizard wizard) {
    wizard = ScriptableObject.CreateInstance<Wizard>();
    wizard.title = "User Interface Wizard";
    wizard.sections = new[]
    {
      new Wizard.Section
      {
        heading = "Introduction",
        text = "Welcome to the wizard."
      },
      new Wizard.Section
      {
        heading = "One",
        text = "Lorem ipsum dolor sit amet."
      },
      new Wizard.Section
      {
        heading = "Two",
        text = "consectetur adipiscing elit."
      }
    };

    AssetDatabase.CreateAsset(wizard, WizardAssetPaths.WizardAssetPath);
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
  }

  static bool DoesAssetExist() {
    return AssetDatabase.LoadAssetAtPath<Wizard>(WizardAssetPaths.WizardAssetPath);
  }

  public static bool DoesLockExist() {
    return Directory.Exists(WizardAssetPaths.WizardLockPath);
  }

  public static void RemoveWizardAsset() {
    if (!DoesAssetExist()) {
      return;
    }

    FileUtil.DeleteFileOrDirectory(WizardAssetPaths.WizardAssetPath);
    FileUtil.DeleteFileOrDirectory(WizardAssetPaths.WizardAssetPath + ".meta");
    AssetDatabase.Refresh();
  }

  public static void RemoveLock() {
    if (!DoesLockExist()) {
      return;
    }

    FileUtil.DeleteFileOrDirectory(WizardAssetPaths.WizardLockPath);
    FileUtil.DeleteFileOrDirectory(WizardAssetPaths.WizardLockPath + ".meta");
    AssetDatabase.Refresh();
  }
}
}
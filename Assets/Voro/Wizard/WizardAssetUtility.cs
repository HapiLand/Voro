using UnityEditor;
using UnityEngine;
using Voro.Internal;

namespace Voro.Wizard {
public static class WizardAssetUtility {
  public static void CreateAndSelectWizard() {
    AssetUtility.GetOrCreateAsset<Wizard>(WizardAssetPaths.WizardPath, out var wizard);
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
    Selection.objects = new Object[] { wizard };
  }
}
}
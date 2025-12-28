using UnityEditor;
using UnityEngine;

namespace Voro.Wizard {
public static class WizardSessionStates {
  static readonly string ShowedWizardStateName = "WizardEditor.showedWizard";

  public static void SetShowedState(bool newState) {
    var oldState = SessionState.GetBool(ShowedWizardStateName, false);
    if (oldState == newState) {
      Debug.LogWarning($"ShowedWizardState: value is already {oldState}");
      return;
    }

    SessionState.SetBool(ShowedWizardStateName, newState);
  }

  public static bool GetShowedState() {
    return SessionState.GetBool(ShowedWizardStateName, false);
  }
}
}
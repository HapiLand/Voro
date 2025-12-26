using UnityEditor;

namespace VoroSystem.UI.Editor {
/// <summary>
/// use this to display the custom fields
/// </summary>
public class UIWindow : EditorWindow {
  AdjustIntegerEditor _adjustIntegerEditor;

  #region Event Functions
  void OnEnable() {
    _adjustIntegerEditor = new AdjustIntegerEditor();
    _adjustIntegerEditor.Initialize();
  }

  void OnGUI() {
    _adjustIntegerEditor?.Draw();
  }
  #endregion

  [MenuItem("Voro/UI Window")]
  public static void ShowWindow() {
    GetWindow<UIWindow>("Window");
  }
}
}
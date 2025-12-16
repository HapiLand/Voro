using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core.Components.Editor.Panels;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor {
public class CoreEditorWindow : EditorWindow {
  #region Serialized Fields
  [SerializeField] Diagram diagram;
  #endregion

  #region Event Functions
  void OnEnable() {
    DiagramEvents.GetInstance().OnDiagramChanged += CreateGUI;
    DiagramEvents.GetInstance().OnCreated += OnDiagramCreated;
  }

  void OnDisable() {
    DiagramEvents.GetInstance().OnDiagramChanged -= CreateGUI;
    DiagramEvents.GetInstance().OnCreated -= OnDiagramCreated;
  }

  void CreateGUI() {
    rootVisualElement.Clear();
    rootVisualElement.Add(new ComputePanel("Voro Compute"));
    if (diagram != null) {
      rootVisualElement.Add(new CorePanel("Voro Core", diagram));
    }
  }
  #endregion

  void OnDiagramCreated(Diagram diagram) {
    this.diagram = diagram;
    CreateGUI();
  }

  public static void ShowWindow() {
    var wnd = GetWindow<CoreEditorWindow>();
    wnd.titleContent = new GUIContent("Voro");
  }
}
}
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Voro.UI.Internal {
class Window : EditorWindow {
    [SerializeField] VisualTreeAsset mVisualTreeAsset;

    // DiagramUI _diagramUI;
    public void CreateGUI() {
        var root = rootVisualElement;
        root.style.flexDirection = FlexDirection.Row;
        // _diagramUI ??= new DiagramUI();
        // root.Add(_diagramUI);

        // _voroGeneration ??= new VoroGeneration(_diagramUI);
        // _newMapButton = new Button { text = "New Map" };
        // root.Add(_newMapButton);
        // _newMapButton.clicked += () => { _voroGeneration.CreateWorldMap(); };
    }


    [MenuItem("Voro/Show Editor Window")]
    public static void ShowGUI() {
        var wnd = GetWindow<Window>();
        wnd.titleContent = new GUIContent("VoroWindow");
    }
}
}
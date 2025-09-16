using EditorGUI.Panels;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorGUI {
public class VoroEditorWindow : EditorWindow {
    public void CreateGUI() {
        var root = rootVisualElement;
        root.style.flexDirection = FlexDirection.Row;

        var editorContents = new VisualElement();
        editorContents.style.flexDirection = FlexDirection.Column;
        editorContents.style.flexGrow = 1;
        root.Add(editorContents);
        editorContents.Add(new Toolbar { name = "WorldManager" });

        var editorCanvas = new VisualElement();
        editorCanvas.style.flexDirection = FlexDirection.Row;
        editorCanvas.style.flexGrow = 1;
        editorContents.Add(editorCanvas);
        editorCanvas.Add(new DiagramCollection { name = "DiagramManager" });
        editorCanvas.Add(new NodeCollection { name = "NodeManager" });
        editorCanvas.Add(new Inspector { name = "InspectorManager" });

        root.Add(new Preview { DisplayName = "Preview" });
    }

    [MenuItem("VoroEditorWindow/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<VoroEditorWindow>();
        wnd.titleContent = new GUIContent("Terrain Generation Editor");
    }
}
}
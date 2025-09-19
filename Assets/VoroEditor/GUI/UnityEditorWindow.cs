using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroEditor.GUI {
public class UnityEditorWindow : UnityEditor.EditorWindow {
    public void CreateGUI() {
        var root = rootVisualElement;
        root.style.flexDirection = FlexDirection.Row;

        // add the Voro Editor to the GUI
        root.Add(new EditorWindow());
    }

    [MenuItem("VoroV4/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<UnityEditorWindow>();
        wnd.titleContent = new GUIContent("Voro Editor v0.4");
    }
}
}
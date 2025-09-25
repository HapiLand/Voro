using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.EditorTabs;

namespace VoroUI {
public class UnityEditorWindow : UnityEditor.EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;

    public void CreateGUI() {
        var root = rootVisualElement;
        root.style.flexDirection = FlexDirection.Row;
        root.Add(new EditorTab());
    }

    [MenuItem("VoroVoroVoroVoro/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<UnityEditorWindow>();
        wnd.titleContent = new GUIContent("EditorWindow");
    }
}
}
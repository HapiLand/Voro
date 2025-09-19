using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroUI {
public class UnityEditorWindow : UnityEditor.EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;

    public void CreateGUI() {
        var root = rootVisualElement;
        root.style.flexDirection = FlexDirection.Row;
        root.Add(new EditorWindow());
    }

    [MenuItem("VoroVoroVoroVoro/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<UnityEditorWindow>();
        wnd.titleContent = new GUIContent("EditorWindow");
    }
}
}
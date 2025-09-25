using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Voro.World;

namespace Voro.UI {
public class UnityEditorWindow : EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;

    void OnEnable() { }

    public void CreateGUI() {
        var root = rootVisualElement;
        root.style.flexDirection = FlexDirection.Row;
        //root.Add(new EditorTab());
    }

    [MenuItem("Voro/Create World")]
    public static void CreateWorld() {
        if (FindFirstObjectByType<VoroWorld>() == null) {
            new GameObject("VoroWorld").AddComponent<VoroWorld>();
        }
        else {
            Debug.LogWarning("A VoroWorldMaster already exists.");
        }
    }


    [MenuItem("Voro/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<UnityEditorWindow>();
        wnd.titleContent = new GUIContent("EditorWindow");
    }
}
}
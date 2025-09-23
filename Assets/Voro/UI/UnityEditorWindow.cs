using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Voro.Grids;
using Voro.Jen;
using Voro.World;

namespace Voro.UI {
public class UnityEditorWindow : EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;
    
    /// <summary>
    ///     encapsulates generation data
    /// </summary>
    Diagram _diagram;

    /// <summary>
    ///     position array for where to instance Chunks
    /// </summary>
    TileMap _tileMap;

    /// <summary>
    ///     executes the generation functions
    /// </summary>
    VoroCompute _voroCompute;

    /// <summary>
    ///     handles events for terrain generation
    /// </summary>
    VoroGeneration _voroGeneration;

    /// <summary>
    ///     GUI to produce terrain generation instructions
    /// </summary>
    VoroUI _voroUI;

    /// <summary>
    ///     instantiate GameObjects into here
    /// </summary>
    VoroWorld _voroWorld;

    /// <summary>
    ///     register events
    /// </summary>
    void OnEnable() {
        Debug.Log("OnEnable");
        EditorSceneManager.sceneOpened += OnSceneOpened;
    }

    void OnDisable() {
        Debug.Log("OnDisable");
        EditorSceneManager.sceneOpened -= OnSceneOpened;
    }

    void CreateGUI() {
        Debug.Log("CreateGUI");
        var root = rootVisualElement;

        _voroUI ??= new VoroUI();
        root.Add(_voroUI);
    }

    void OnSceneOpened(Scene scene, OpenSceneMode mode) {
        CreateComponents();
    }

    void CreateComponents() {
        Debug.Log("Create Components");

        // dispose 
        _voroUI?.Dispose();
        _diagram?.Dispose();
        _voroWorld?.Dispose();
        _voroGeneration?.Dispose();

        // create Diagram & dependencies
        _tileMap = new TileMap();
        _voroUI ??= new VoroUI();
        _diagram = new Diagram(_tileMap, _voroUI);

        // Generator
        _voroWorld = new GameObject("VoroWorld").AddComponent<VoroWorld>();
        _voroCompute = new VoroCompute(_diagram);
        _voroGeneration = new VoroGeneration(_voroWorld, _voroCompute, _voroUI);
    }


    [MenuItem("Voro/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<UnityEditorWindow>();
        wnd.titleContent = new GUIContent("EditorWindow");
    }
}
}
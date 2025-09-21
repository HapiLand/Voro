using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace VoroUI {
public static class EditorEvents {
    public static event Action OnSceneReloaded;
    // todo add events to this class

    [MenuItem("VoroVoroVoroVoro/Reload Scene")]
    public static void ReloadGameWorldScene() {
        OnSceneReloaded?.Invoke();
        EditorSceneManager.OpenScene("Assets/VoroWorld/WorldScene.unity", OpenSceneMode.Single);
    }
}
}
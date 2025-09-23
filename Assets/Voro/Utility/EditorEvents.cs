using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Voro.Utility {
public static class EditorEvents {
    public static event Action OnSceneReloaded;

    [MenuItem("Voro/Reload Scene")]
    public static void ReloadGameWorldScene() {
        OnSceneReloaded?.Invoke();
        EditorSceneManager.OpenScene("Assets/Voro/WorldScene.unity", OpenSceneMode.Single);
    }
}
}
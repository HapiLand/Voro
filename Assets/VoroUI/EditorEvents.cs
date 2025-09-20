using System;
using UnityEditor.SceneManagement;

namespace VoroUI {
public static class EditorEvents {
    public static event Action OnSceneReloaded;

    public static void ReloadGameWorldScene() {
        OnSceneReloaded?.Invoke();
        EditorSceneManager.OpenScene("Assets/Unity/Scenes/GameWorld.unity", OpenSceneMode.Single);
    }
}
}
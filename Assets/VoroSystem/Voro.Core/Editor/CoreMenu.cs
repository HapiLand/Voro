using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Designer;
using VoroSystem.Voro.World;

namespace VoroSystem.Voro.Core.Editor {
public class CoreMenu : EditorWindow {
    [MenuItem("VoroCore/Open Designer")]
    public static void CreateDesigner() {
        Create<VoroDesigner>();
    }

    [MenuItem("VoroCore/New World")]
    public static void CreateWorld() {
        Create<VoroWorld>();
    }

    static void Create<T>() where T : Component {
        var component = FindAnyObjectByType<T>();
        if (component != null) {
            return;
        }

        Selection.activeObject = new GameObject().AddComponent<T>();
    }
}
}
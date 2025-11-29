using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;

namespace VoroSystem.Voro.World.Editor {
[InitializeOnLoad]
[CustomEditor(typeof(VoroMap))]
public class VoroMapDrawer : UnityEditor.Editor {
    static readonly BoxBoundsHandle BoundsHandle = new();

    static VoroMapDrawer() {
        SceneView.duringSceneGui -= OnGlobalSceneGUI;
        SceneView.duringSceneGui += OnGlobalSceneGUI;
    }

    static void OnGlobalSceneGUI(SceneView sceneView) {
        var map = FindAnyObjectByType<VoroMap>();
        if (map != null) {
            DrawHandles(map);
        }
    }

    static void DrawHandles(VoroMap map) {
        var a = map.cornerA.ToVector3();
        var b = map.cornerB.ToVector3();

        var min = Vector3.Min(a, b);
        var max = Vector3.Max(a, b);
        var center = (min + max) * 0.5f;
        var size = max - min;

        Handles.color = Color.white;

        EditorGUI.BeginChangeCheck();

        BoundsHandle.center = center;
        BoundsHandle.size = size;
        BoundsHandle.SetColor(Color.white);

        using (new Handles.DrawingScope(Matrix4x4.identity)) {
            BoundsHandle.DrawHandle();
        }

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(map, "Resize Bounds");

            var newMin = BoundsHandle.center - BoundsHandle.size * 0.5f;
            var newMax = BoundsHandle.center + BoundsHandle.size * 0.5f;

            map.cornerA = newMin.ToVector2();
            map.cornerB = newMax.ToVector2();

            EditorUtility.SetDirty(map);
            SceneView.RepaintAll();
        }

        DrawTiles(map);
    }

    static void DrawTiles(VoroMap map) {
        map.ForEach(chunk => {
            var center = chunk.Position.ToVector3() + new Vector3(chunk.Size * 0.5f, 0f, chunk.Size * 0.5f);
            const float scale = 0.95f;
            var size = new Vector3(chunk.Size * scale, 0f, chunk.Size * scale);
            Handles.color = chunk.Visible ? Color.forestGreen : Color.crimson;
            Handles.color *= new Color(1f, 1f, 1f, 0.1f);
            
            Handles.DrawWireCube(center, size);
        });
    }
}
}
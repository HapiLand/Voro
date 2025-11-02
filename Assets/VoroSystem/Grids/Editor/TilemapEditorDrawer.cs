using UnityEditor;
using UnityEngine;
using VoroSystem.Extensions;
using VoroSystem.TilemapSystem;

namespace VoroSystem.Grids.Editor {
[CustomEditor(typeof(BasicTilemapComponent))]
public class TilemapEditorDrawer : UnityEditor.Editor {
    /// <summary> Visualise the Tilemap as a Grid </summary>
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    static void OnSceneGUI(BasicTilemapComponent t, GizmoType gizmoType) {
        if (t == null) {
            return;
        }

        // var map = t.Tilemap;

        for (var y = 0; y < t.TilemapParameters.mapSizeY; y++) {
            for (var x = 0; x < t.TilemapParameters.mapSizeX; x++) {
                var tile = t.CompMap.Tilemap.GetTile(x, y);
                var center = tile.Position.ToVector3();
                var size = new Vector3(t.TilemapParameters.tileSize * 0.95f, 0f, t.TilemapParameters.tileSize * 0.95f);
                Handles.color = tile.Visible ? Color.lawnGreen : Color.crimson;
                Handles.DrawWireCube(center, size);
            }
        }
    }
}
}
using Internal;
using UnityEngine;

namespace ConfigEditor.V2 {
public class WorldTile {
    readonly (int x, int z) _origin;
    // Todo check if the WorldTile is visible to the scene camera
    // ToDo initialise the WorldTile once when it becomes visible

    public bool HasInitialised;
    public bool IsVisible;

    /// <summary>
    ///     the voro diagram with the point data that represents this tile
    /// </summary>
    public Diagram VoroDiagram;
    // ToDo rename Diagram to VoroDiagram

    public WorldTile(int x, int z) {
        _origin = (x, z);
        IsVisible = true;
        HasInitialised = false;

        VisibleFirstTime();
    }

    public override string ToString() {
        return $"WorldTile [{_origin.x},{_origin.z}]";
    }

    /// <summary>
    ///     called when the tile becomes visible for the first time
    /// </summary>
    void VisibleFirstTime() {
        ConstructDiagram();
        HasInitialised = true;
    }

    void ConstructDiagram() {
        //Debug.Log($"Create VoroDiagram [{_origin.x}, {_origin.z}]");
        VoroDiagram = new Diagram(_origin);

        var cells = ResourceHelper.CreateCellArray();
        VoroDiagram.PointMap = new int[cells.Length];
        VoroDiagram.Points = new Vector3[cells.Length];
        VoroDiagram.GeoMap = new int[cells.Length];
        VoroDiagram.Geometry = new GameObject[cells.Length];
        for (var i = 0; i < cells.Length; i++) {
            var cell = cells[i];
            VoroDiagram.PointMap[i] = i;
            VoroDiagram.Points[i] = cell.position;
            // ToDo set point color
            VoroDiagram.GeoMap[i] = cell.id;
            VoroDiagram.Geometry[i] = Resources.Load<GameObject>($"FBX/{i}_0");
            var mat = Resources.Load<Material>("FbxMat");
            var matClone = new Material(mat)
            {
                color = new Color(1, 1, 1, 1)
            };
            var renderer = VoroDiagram.Geometry[i].GetComponent<MeshRenderer>();
            renderer.material = matClone;
        }

        Debug.Log(VoroDiagram.ToString());

        // diagram has been constructed, EditorCompute must execute to set the true position
        // ToDo run EditorCompute on the newly constructed VoroDiagram
    }
}
}
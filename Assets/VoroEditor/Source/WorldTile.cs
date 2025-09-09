using UnityEngine;
using VoroEditor.Utility;

namespace VoroEditor.Source {
public class WorldTile {
    readonly (int x, int z) _origin;

    /// <summary>
    ///     the voro diagram with the point data that represents this tile
    /// </summary>
    public VoroDiagram Diagram;
    // Todo check if the WorldTile is visible to the scene camera
    // ToDo initialise the WorldTile once when it becomes visible

    public bool HasInitialised;
    public bool IsVisible;

    /// <summary>
    ///     this is the GameObject that will contain all the geometry for the diagram
    /// </summary>
    public GameObject TileContainer;

    public WorldTile(int x, int z) {
        Debug.Log($"Creating tile {x},{z}");
        _origin = (x, z);
        Diagram = new VoroDiagram(_origin);

        IsVisible = true;
        HasInitialised = false;

        // container object for the geometry
        TileContainer = new GameObject($"WorldTile [{x},{z}]");


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
        var cells = ResourceHelper.CreateCellArray();
        Diagram.PointMap = new int[cells.Length];
        Diagram.Points = new Vector3[cells.Length];
        Diagram.GeoMap = new int[cells.Length];
        Diagram.Geometry = new GameObject[cells.Length];
        for (var i = 0; i < cells.Length; i++) {
            var cell = cells[i];
            Diagram.PointMap[i] = i;

            // set the position of the point
            // cell.position is the local coordinate of the point
            // the cell position comes from a PointTable.json which has positions in a UV space
            Diagram.Points[i] = cell.position;
            // origin is the world position of the tile
            Diagram.Points[i] += new Vector3(_origin.x, 0f, _origin.z);
            // ToDo set point color from data
            Diagram.GeoMap[i] = cell.id;

            // add instances of the geometry to the diagram
            ResourceHelper.LoadAndInstanceResource($"FBX/{i}_0", out Diagram.Geometry[i]);
            Diagram.Geometry[i].name = $"[{i}  0]";
            var mat = Resources.Load<Material>("FbxMat");
            var matClone = new Material(mat)
            {
                color = Color.ghostWhite
            };
            var renderer = Diagram.Geometry[i].GetComponent<MeshRenderer>();
            renderer.material = matClone;
            // set the position of the geometry to match the point
            Diagram.Geometry[i].transform.position += Diagram.Points[i];

            // set the parent of the geometry inside the container
            Diagram.Geometry[i].transform.SetParent(TileContainer.transform);
        }

        Debug.Log(Diagram.ToString());
    }
}
}
using UnityEngine;
using VoroWorld.Utility;

namespace VoroWorld.Grids {
/// <summary>
///     represents the location of a square tile that is part of a grid
///     ---
///     WorldTile is related to the Diagram type
///     because the WorldTile exists as an object inside the Unity Scene
///     it shall contain the PointTable data in the form of a VoroDiagram
///     the WorldTile shall store the table of points for the cell geometry
///     the table of points produces a VoroDiagram which is the actual data to produce the Voro e
/// </summary>
public class WorldTile {
    readonly (int x, int z) _origin;
    public VoroDiagram Diagram;

    public WorldTile(int x, int z) {
        _origin = (x, z);
        IsVisible = true;
        HasInitialised = false;

        TileContainer = new GameObject($"WorldTile [{x},{z}]");
        TileContainer.transform.position = new Vector3(x, 0, z);

        Diagram = new VoroDiagram(0, OnDiagramLoaded);
    }

    public bool HasInitialised { get; private set; }
    public bool IsVisible { get; private set; }
    public GameObject TileContainer { get; }

    public (int x, int z) Origin => _origin;

    void OnDiagramLoaded(VoroDiagram diagram) {
        HasInitialised = true;

        for (var i = 0; i < diagram.CellPoints.Length; i++) {
            var localCellPoint = diagram.CellPoints[i];
            var worldPosition = new Vector3(
                localCellPoint.Position.x + _origin.x,
                localCellPoint.Position.y,
                localCellPoint.Position.z + _origin.z
            );

            var cellObjectInstance = new GameObject($"Cell [{localCellPoint.ID}]");
            var mf = cellObjectInstance.AddComponent<MeshFilter>();
            var mr = cellObjectInstance.AddComponent<MeshRenderer>();

            // instance the Mesh instance for this point
            var helper = MeshLibraryHelper.Instance;
            var meshes = helper.GetMeshArray(localCellPoint.ID);
            Debug.Log($"CellPoint {localCellPoint.ID} loaded {meshes.Length} meshes");
            // select a mesh at random
            var randIndex = Random.Range(0, meshes.Length); // [0, Length]
            var meshInstance = meshes[randIndex];
            mf.mesh = meshInstance;

            // set color and material for the object
            var mat = Resources.Load<Material>("FbxMat");
            var matClone = new Material(mat)
            {
                // read configuration for the color of this point
                color = diagram.Configuration.PointColors[i]
            };
            mr.material = matClone;

            // set world position of the mesh object
            cellObjectInstance.transform.position = worldPosition;
            cellObjectInstance.transform.SetParent(TileContainer.transform);
        }
    }
}
}
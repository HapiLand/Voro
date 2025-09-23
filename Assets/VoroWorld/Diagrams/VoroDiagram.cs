using Source.Utility;
using UnityEngine;
using VoroWorld.Grids;

namespace VoroWorld.Diagrams {
/// <summary>
///     contains the Tile instance and a collection of Voro Points
///     VoroDiagram is what gets used in VoroCompute
/// </summary>
public class VoroDiagram {
    public Point[] CellPoints;
    public Config Configuration;
    public Tile Tile; // todo decouple from Tile

    /// <summary>
    ///     all VoroDiagrams in the world have been created
    ///     instantiation of the GameObjects will happen now
    /// </summary>
    /// <param name="container"></param>
    public void WorldManagerOnOnCreatedAllDiagrams(Transform container) {
        // set the parent of the tile game object
        Tile.Container.transform.SetParent(container);

        // create the Mesh instances
        for (var i = 0; i < CellPoints.Length; i++) {
            // get the point and its position in the world
            var point = CellPoints[i];
            var localPosition = point.Position; // Point.Position is in a local UV space
            var worldPosition = localPosition + Tile.Position; // Tile.Position is in global World space

            // create the GameObject to hold the mesh
            var pointID = point.ID;
            var cellObjectInstance = new GameObject($"Cell [{pointID}]");
            var mf = cellObjectInstance.AddComponent<MeshFilter>();
            var mr = cellObjectInstance.AddComponent<MeshRenderer>();

            // instance the mesh
            var helper = MeshLibraryHelper.Instance;
            var meshes = helper.GetMeshArray(pointID);
            // Debug.Log($"CellPoint {localCellPoint.ID} loaded {meshes.Length} meshes");

            // select a mesh at random
            var randIndex = Random.Range(0, meshes.Length); // [0, Length]
            var meshInstance = meshes[randIndex];
            mf.mesh = meshInstance;

            // set color and material for the object
            var mat = Resources.Load<Material>("FbxMat");
            var matClone = new Material(mat)
            {
                // read configuration for the color of this point
                color = Configuration.PointColors[i]
            };
            mr.material = matClone;

            // set world position of the mesh object
            cellObjectInstance.transform.position = worldPosition;
            cellObjectInstance.transform.SetParent(Tile.Container.transform);
        }
    }

    /// <summary>
    ///     creates a Tile in the VoroDiagram
    /// </summary>
    /// <param name="x">world position x</param>
    /// <param name="z">world position z</param>
    public void CreateTile(int x, int z) {
        var pos = new Vector3(x, 0f, z);
        Tile = new Tile(pos);
    }

    public void SetCellPoints(TablePoint[] tablePoints) {
        Configuration = new Config
        {
            PointColors = new Color[tablePoints.Length]
        };

        CellPoints = new Point[tablePoints.Length];
        for (var i = 0; i < tablePoints.Length; i++) {
            var point = tablePoints[i];
            // set the local position of the point
            var position = new Vector3(point.Pos[0], 0, point.Pos[1]);
            CellPoints[i] = new Point(position, point.Id);

            var color = point.Col;
            Configuration.PointColors[i] = new Color(color[0], color[1], color[2], 1.0f);
        }
    }

    public struct Config {
        public Color[] PointColors;
    }
}
}
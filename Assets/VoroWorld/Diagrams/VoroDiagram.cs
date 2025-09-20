using Source.Utility;
using UnityEngine;
using VoroWorld.Grids;

namespace VoroWorld.Diagrams {
/// <summary>
///     contains the Tile instance and a collection of Voro Points
///     VoroDiagram is what gets used in VoroCompute
/// </summary>
public class VoroDiagram {
    Point[] _cellPoints;
    Config _configuration;
    Tile _tile;

    public VoroDiagram() {
        WorldManager.OnCreatedAllDiagrams += WorldManagerOnOnCreatedAllDiagrams;
    }

    /// <summary>
    ///     all VoroDiagrams in the world have been created
    ///     instantiation of the GameObjects will happen now
    /// </summary>
    /// <param name="container"></param>
    void WorldManagerOnOnCreatedAllDiagrams(Transform container) {
        // set the parent of the tile game object
        _tile.Container.transform.SetParent(container);

        // create the Mesh instances
        for (var i = 0; i < _cellPoints.Length; i++) {
            // get the point and its position in the world
            var point = _cellPoints[i];
            var localPosition = point.Position; // Point.Position is in a local UV space
            var worldPosition = localPosition + _tile.Position; // Tile.Position is in global World space

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
                color = _configuration.PointColors[i]
            };
            mr.material = matClone;

            // set world position of the mesh object
            cellObjectInstance.transform.position = worldPosition;
            cellObjectInstance.transform.SetParent(_tile.Container.transform);
        }
    }

    /// <summary>
    ///     creates a Tile in the VoroDiagram
    /// </summary>
    /// <param name="x">world position x</param>
    /// <param name="z">world position z</param>
    public void CreateTile(int x, int z) {
        var pos = new Vector3(x, 0f, z);
        _tile = new Tile(pos);
    }

    public void SetCellPoints(TablePoint[] tablePoints) {
        _configuration = new Config
        {
            PointColors = new Color[tablePoints.Length]
        };

        _cellPoints = new Point[tablePoints.Length];
        for (var i = 0; i < tablePoints.Length; i++) {
            var point = tablePoints[i];
            // set the local position of the point
            var position = new Vector3(point.Pos[0], 0, point.Pos[1]);
            _cellPoints[i] = new Point(position, point.Id);

            var color = point.Col;
            _configuration.PointColors[i] = new Color(color[0], color[1], color[2], 1.0f);
        }
    }

    public struct Config {
        public Color[] PointColors;
    }
}
}
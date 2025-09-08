using Internal;
using UnityEngine;

namespace Terrain {
/// <summary>
///     utility to make creating diagrams easy
/// </summary>
public class DiagramFactory {
    /// <summary>
    ///     a placeholder diagram is used for testing EditorComputer
    ///     this diagram will be populated with data for testing
    /// </summary>
    /// <returns></returns>
    public VoroDiagram CreatePlaceholder() {
        var diagram = new VoroDiagram();

        // populate diagram with data
        diagram.PointMap = new int[1]
        {
            0
        };
        diagram.Points = new (Vector3, int)[1]
        {
            (new Vector3(0, 0, 0), 0)
        };

        return diagram;
    }

    public (Vector3, GameObject)[] GetDiagramGeometry(VoroDiagram diagram) {
        var pointObjects = new (Vector3, GameObject)[diagram.PointMap.Length];
        for (var i = 0; i < diagram.PointMap.Length; i++) {
            var geoIndex = diagram.GeoMap[i];
            var instance = diagram.Geometry[geoIndex];

            var pointIndex = diagram.PointMap[i];
            var point = diagram.Points[pointIndex];
            var position = point.Item1;

            pointObjects[i] = (position, instance);
        }

        return pointObjects;
    }

    /// <summary>
    ///     this creates a diagram to be used in Tile
    /// </summary>
    /// <returns></returns>
    public VoroDiagram Create(Vector2 origin) {
        // ToDo construct the Diagram here same as Tile VoroInstance = new Voro(_corner); does

        Debug.Log("Create VoroDiagram");
        var diagram = new VoroDiagram();
        diagram.Origin = origin;

        // create the point data in the diagram
        var cells = ResourceHelper.CreateCellArray();
        diagram.PointMap = new int[cells.Length];
        diagram.Points = new (Vector3, int)[cells.Length];
        diagram.GeoMap = new int[cells.Length];
        diagram.Geometry = new GameObject[cells.Length];
        for (var i = 0; i < cells.Length; i++) {
            var cell = cells[i];
            diagram.PointMap[i] = i;
            diagram.Points[i] = (cell.position, cell.id);
            // ToDo set point color
            diagram.GeoMap[i] = cell.id;
            diagram.Geometry[i] = Resources.Load<GameObject>($"FBX/{i}_0");
            var mat = Resources.Load<Material>("FbxMat");
            var matClone = new Material(mat)
            {
                color = new Color(1, 1, 1, 1),
            };
            var renderer = diagram.Geometry[i].GetComponent<MeshRenderer>();
            renderer.material = matClone;
        }

        // diagram has been constructed, EditorCompute must execute to set the true position
        // ToDo run EditorCompute on the newly constructed VoroDiagram

        return diagram;
    }
}
}
using Internal;
using UnityEngine;

namespace ConfigEditor.V2 {
/// <summary>
///     utility so Unity GameObjects can easily be created by WorldManager
/// </summary>
public class GameObjectFactory {
    public void CreateFromWorldTile(WorldTile tile, out GameObject container) {
        // container will contain the fbx pieces within the diagram
        container = tile.TileContainer;

        return;

        var diagram = tile.Diagram;

        // use the diagram maps to get the position and mesh for each piece
        var geoPieces = new (Vector3, GameObject)[diagram.PointMap.Length];
        for (var i = 0; i < diagram.PointMap.Length; i++) {
            var geoIndex = diagram.GeoMap[i];
            var instance = diagram.Geometry[geoIndex];
            var pointIndex = diagram.PointMap[i];
            var position = diagram.Points[pointIndex];
            geoPieces[i] = (position, instance);
        }

        // instantiate each piece of geometry
        foreach (var (position, geo) in geoPieces) {
            ResourceHelper.InstanceGeometry<GameObject>(geo, out var geoInstance);

            // set position of the geometry
            // offset to the actual scene position that the world tile has
            geoInstance.transform.position += new Vector3(diagram.Origin.x, 0f, diagram.Origin.z);
            // offset to the local position the point has in the diagram
            geoInstance.transform.position += position;
        }
    }
}
}
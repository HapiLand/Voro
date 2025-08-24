namespace DataTypes {
public class GeometryArray {
    public readonly Geometry[] Geo;

    public GeometryArray(PointArray pointArr) {
        // get the mesh objects for the cells of the voro
        Geo = new Geometry[pointArr.points.Length];
        for (var i = 0; i < Geo.Length; i++) {
            var geo = new Geometry(pointArr.points[i].id, pointArr.points[i].color);
            // ToDo store a dictionary of <id,fbx[]> elsewhere, each id for every variant
            Geo[i] = geo;
        }
    }
}
}
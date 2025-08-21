using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
public class PointArray {
    
    public Point[] points;
    
    public PointArray(TextAsset data) {
        // the data stores an array of <id,pos> for every point
        // PointArray reads that data and translates it into
        // the array of Points which represent the json values
    
        // generate an array of points from the json data
        var fromJson = JsonUtility.FromJson<JsonPointArray>(data.text);

        // get the point data from the array
        var jsonPoints = fromJson.points;
        
        // generate the point data
        points = new Point[jsonPoints.Length];
        for (var i = 0; i < jsonPoints.Length; i++) {
            
            // data also contains a configuration for how to set the voro height
            // the height is set after the voro has been constructed with all points
            // if this was to change so points are constructed with a height value
            // the height can be solved here, and used for constructing each point
            
            // create a new point
            var point = new Point(jsonPoints[i]);
            points[i] = point;
        }
        

    }
}
}
using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
public class PointArray {
    
    public Point[] points;
    
    public PointArray(TextAsset data) {
    
        // generate an array of points from the json data
        var fromJson = JsonUtility.FromJson<JsonPointArray>(data.text);

        // get the point data from the array
        var jsonPoints = fromJson.points;
        
        // generate the point data
        points = new Point[jsonPoints.Length];
        for (var i = 0; i < jsonPoints.Length; i++) {
            // create a new point
            var point = new Point(jsonPoints[i]);
            points[i] = point;
        }
    }
}
}
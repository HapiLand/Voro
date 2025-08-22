using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace DataTypes {
[Serializable]
public class PointArray {
    
    public Point[] points;
    
    public PointArray(TextAsset data) {
        // the data stores an array of <id,pos> for every point
        // PointArray reads that data and translates it into
        // the array of Points which represent the json values
    
        // unity JsonUtility is no more, long live Newtonsoft
        // directly parse json points into array
        JsonPoint[] pointArray = JObject.Parse(data.text)["points"].ToObject<JsonPoint[]>();
        
        // generate the point data
        points = new Point[pointArray.Length];
        for (var i = 0; i < points.Length; i++) {
            // data also contains a configuration for how to set the voro height
            // the height is set after the voro has been constructed with all points
            // if this was to change so points are constructed with a height value
            // the height can be solved here, and used for constructing each point
            
            // create a new point
            var point = new Point(pointArray[i]);
            points[i] = point;
        }

    }
}
}
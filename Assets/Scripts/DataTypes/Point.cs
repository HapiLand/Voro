using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
public struct Point {
    // Point[id,x,height,z,convergence,col]
    public int id;
    public Vector3 position;
    public Color Color {
        get
        {
            var t = Mathf.Abs(position.y) % 1;
            return Color.Lerp(Color.cornflowerBlue, Color.crimson, t);
        }
    }
    public Point(JsonPoint data) {
        // set this point as the json point
        id = data.id;
        var x = data.p[1];
        var z = data.p[0];
        position = new Vector3(x, 0, z);
    }
}
}
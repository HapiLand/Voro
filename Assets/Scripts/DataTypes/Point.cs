using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
public struct Point {
    // Point[id,x,z,convergence,col]
    public int id;
    public Vector3 position;
    public Color color;
    
    /*public Color Color {
        get
        {
            var t = Mathf.Abs(position.y) % 1;
            return Color.Lerp(Color.cornflowerBlue, Color.crimson, t);
        }
    }*/
    public Point(JsonPoint data) {
        // set this point as the json point
        id = data.id;
        var x = data.p[1];
        var z = data.p[0];
        position = new Vector3(x, 0, z);
        color = new Color(data.col[0], data.col[1], data.col[2], 1.0f);
        // point is constructed with y=0, its height is set after all points have been constructed
    }
}
}
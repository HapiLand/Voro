using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
public struct Point {
    // Point[id,x,z,coverage,col]
    
    public int id;
    public Vector3 position;
    public Color color;
    
    // ToDo coverage
    // each Point has an associated Geometry, the source for both of these
    // comes from a generated Voronoi Diagram (to produce Table.json)
    // https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Coloured_Voronoi_3D_slice.svg/1280px-Coloured_Voronoi_3D_slice.svg.png
    // many of these cells have a similar shape to other cells. if two cells
    // happen to be 100% identical it would be wasteful to generate two fbx meshes
    // the generated points can be given a coverage value, coverage means
    // when both cells are overlayed together, how much does one cell cover the other
    // meaning similar cells have high coverage, a triangle and a circle would have low coverage
    // when Geometry is created, the coverage of this point is written to the Geometry struct
    // when the MeshInstance GameObject is instantiated to the Unity scene
    // any Geometry objects with a similar coverage value can share the same mesh
    // this is important for reducing the total amount of mesh files that should be generated
    
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
        // point is constructed with y=0, its height is set after all points have been constructed
        
        // JsonPoint data contains random point color
        // ToDo allow color to have some randomness to it so its not always uniform
        // will be a good idea to set a random color in this struct
        color = new Color(data.col[0], data.col[1], data.col[2], 1.0f);
        
    }
}
}
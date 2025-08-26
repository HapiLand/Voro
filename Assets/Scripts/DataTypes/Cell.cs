using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DataTypes {
/// <summary>
///  A combination of a Point and Geometry
/// </summary>
[Serializable]
public struct Cell {
    public int id;
    public Vector3 position;
    public Color color;
    public GameObject[] fbxArray;

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
    
    public GameObject GetFBX() {
        // pick the mesh for this cell
        var variants = fbxArray.Length;
        var variant = Random.Range(0, variants);
        var instance = fbxArray[variant];
        //prefab.transform.position no longer set here

        // set prefab color
        var mat = Resources.Load<Material>("FbxMat");

        var matClone = new Material(mat);
        matClone.color = color;
        

        var renderer = instance.GetComponent<MeshRenderer>();
        renderer.material = matClone;

        return instance;
    }
    
    public Cell(JsonPoint point) {
        id = point.id;
        // ToDo fix DemoTable.json so it stores the position as XZ instead of currently as ZX
        position = new Vector3(point.p[1], 0, point.p[0]);
        // ToDo allow color to have some randomness to it so its not always uniform
        color = new Color(point.col[0], point.col[1], point.col[2], 1.0f);
        
        const int variants = 3;
        fbxArray = new GameObject[variants];
        for (var i = 0; i < variants; i++) {
            fbxArray[i] = Resources.Load<GameObject>($"FBX/{id}_{i}");
        }
    }
}
}
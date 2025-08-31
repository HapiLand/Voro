using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DataTypes {
/// <summary>
///     A combination of a Point and Geometry
/// </summary>
[Serializable]
public struct Cell {
    public int id;
    public Vector3 position;
    public Color color;
    public GameObject[] fbxArray;
    // ToDo coverage

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
        color = new Color(point.col[0], point.col[1], point.col[2], 1.0f);
        // subtract randomness to the color
        var rand = Random.value * 0.5f;
        color -= new Color(rand, rand, rand, 0.0f);


        //var t = Mathf.Abs(position.y) % 1;
        //return Color.Lerp(Color.cornflowerBlue, Color.crimson, t);

        const int variants = 3;
        fbxArray = new GameObject[variants];
        for (var i = 0; i < variants; i++) {
            fbxArray[i] = Resources.Load<GameObject>($"FBX/{id}_{i}");
        }
    }
}
}
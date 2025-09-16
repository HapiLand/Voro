using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EditorGUI.Source.Voro {
[Serializable]
public struct Cell {
    public int id;
    public Vector3 position;
    public Color color;
    public GameObject[] fbxArray;
    public int GroupID;
    // ToDo coverage

    public GameObject GetFBX() {
        // pick the mesh for this cell
        var variants = fbxArray.Length;
        variants = 0;
        var variant = Random.Range(0, variants);
        var instance = fbxArray[variant];

        // set prefab color
        var mat = Resources.Load<Material>("FbxMat");

        var matClone = new Material(mat)
        {
            color = color
        };

        var renderer = instance.GetComponent<MeshRenderer>();
        renderer.material = matClone;

        return instance;
    }

    public Cell(JsonPoint point) {
        id = point.id;

        position = new Vector3(point.p[0], 0, point.p[1]);

        color = new Color(point.col[1], point.col[0], point.col[2], 1.0f);

        // subtract randomness to the color
        //var rand = Random.value * 0.5f;
        //color -= new Color(rand, rand, rand, 0.0f);


        //var t = Mathf.Abs(position.y) % 1;
        //return Color.Lerp(Color.cornflowerBlue, Color.crimson, t);
        GroupID = -1;

        const int variants = 3;
        fbxArray = new GameObject[variants];
        for (var i = 0; i < variants; i++) {
            fbxArray[i] = Resources.Load<GameObject>($"FBX/{id}_{i}");
        }
    }
}
}
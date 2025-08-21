using UnityEngine;

namespace DataTypes {
public readonly struct Geometry {
    // Geometry[id,dict<id,fbx[]>]
    readonly int _id;
    public GameObject MeshInstance {
        get
        {
            // pick the mesh for this cell
            const int variants = 0;
            var variant = Random.Range(0, variants);
            
            var instance = Resources.Load<GameObject>($"FBX/{_id}_{variant}");
            //prefab.transform.position no longer set here
            
            // set prefab color
            var mat = Resources.Load<Material>("FbxMat");
            mat.color = Color.sandyBrown;
            var renderer = instance.GetComponent<MeshRenderer>();
            renderer.material = mat;
            
            return instance;
        }
    }
    
    public Geometry(int id) {
        _id = id;
    }
}
}
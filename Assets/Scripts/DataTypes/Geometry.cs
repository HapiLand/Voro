using UnityEngine;

namespace DataTypes {
public readonly struct Geometry {
    // Geometry[id,dict<id,fbx[]>]
    
    readonly int _id;
    readonly GameObject[] _fbxObjects;
    readonly Color _color;
    
    public GameObject MeshInstance {
        get
        {
            // pick the mesh for this cell
            var variants = _fbxObjects.Length;
            var variant = Random.Range(0, variants);
            var instance = _fbxObjects[variant];
            //prefab.transform.position no longer set here
            
            // set prefab color
            var mat = Resources.Load<Material>("FbxMat");
            
            var matClone = new Material(mat);
            matClone.color = _color;
            
            var renderer = instance.GetComponent<MeshRenderer>();
            renderer.material = matClone;
            
            return instance;
        }
    }
    
    public Geometry(int id, Color color) {
        // ToDo new Geometry should be given fbx[] for its constructor
        _id = id;
        
        // set the mesh color from the value in the point
        _color = color;
        
        // store all the fbx instances that the geometry can use
        const int variants = 3;
        _fbxObjects = new GameObject[variants];
        for (var i = 0; i < variants; i++) {
            // the Resources directory contains a large collection of .fbx files
            // the id of this struct matches each fbx. for id=8 this is 8_x.fbx
            // the second value _x.fbx is the variation of that piece, _0.fbx _1.fbx _2.fbx
            
            // the purpose of variants is to have slight changes on the way the mesh looks
            // so that if there are multiple Geometry structs with the same id value
            // a GameObject created for each one can have a different appearance
            _fbxObjects[i] = Resources.Load<GameObject>($"FBX/{_id}_{i}");
            // note: currently each variants are all identical
            
        }
    }
}
}
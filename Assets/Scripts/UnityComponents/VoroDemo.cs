using System;
using DataTypes;
using Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityComponents {
public class VoroDemo : MonoBehaviour {

    Voro _voro;
    public Voro Voro {
        get { return _voro; }
    }
    
    // makes the Voro Geometry use a single mesh which can better be instanced
    // for testing purposes, a Voro does not need its complex geometry 
    // currently broken
    [SerializeField] bool _useDebugMesh;
    
    Material _mat;
    Mesh _mesh;

    void Awake() {
        _mat = Resources.Load<Material>("FbxMat");
        _mesh = ResourceHelper.LoadResource<Mesh>("DebugMesh");

        _voro = ResourceHelper.CreateVoro(transform,"MyConfig");
    }

    void Start() {
        var configName = "MyConfig";

        // construct the voro
        // the voro is given the name of the config, this controls how the height is generated
        // also set the position for where the voro is created
        _voro = new Voro(configName, transform);
    }

    void Update() {
        _voro.Update();
    }

    /*
    void Update() {
        DrawDebugVoroMesh();
    }

    void DrawDebugVoroMesh() {
        if (!_useDebugMesh) {
            // do nothing as this debug is not enabled
            return;
        }

        // generate the voro using a shared mesh for each piece
        // this is cheaper to run, good for testing purposes
        // the resulting geometry wouldnt be used for a game

        var numGeos = _voro.Geometry.Length;
        var rp = new RenderParams(_mat);
        var instData = new Matrix4x4[numGeos];
        for (var i = 0; i < numGeos; ++i) {
            // get the location of the point, which is where the geo is created
            // _voro.Points[i].position exists in a local space to the voro
            // {0,0} is the origin of the voro, which is the bottom-left corner
            // the local space extends to {1,1} which is the opposite corner
            // offset the position using the VoroDemo GameObject position
            // this moves the voros local position to the actual location in the game world
            var pos = _voro.Points[i].position + transform.position;
            instData[i] = Matrix4x4.Translate(pos);
        }

        Graphics.RenderMeshInstanced(rp, _mesh, 0, instData);
    }
    */
}
}
using System;
using DataTypes;
using Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityComponents {
public class VoroDemo : MonoBehaviour {

    Voro _voro;
    
    // makes the Voro Geometry use a single mesh which can better be instanced
    // for testing purposes, a Voro does not need its complex geometry 
    [SerializeField] bool _useDebugMesh;
    Material _mat;
    Mesh _mesh;

    void Awake() {
        _mat = Resources.Load<Material>("FbxMat");
        _mesh = ResourceHelper.LoadResource<Mesh>("DebugMesh");
    }

    void Start() {
        
        // this is the variable for a .json file
        // previously this is so the user can tell the Voro what collection of Points to use
        // Voro is now in control of that task, the user no longer has this control
        //var data = ResourceHelper.LoadResource<TextAsset>("Points/DemoTable");
        // if the new approach to take is for the Voro to auto ConfigurePointHeight
        // it is more reasonable to get the user to select the MyConfig.json file
        // the user decides how the terrain should look, MyConfig does that

        // the user provides the config file to the Voro
        // this allows the Voro to be generated with the look that the config gives instruction for
        var configName = "MyConfig";

        // construct the voro
        // the voro is given the name of the config, this controls how the height is generated
        // also set the position for where the voro is created
        _voro = new Voro(configName, transform.position);
        
        // ToDo too yucky for the VoroDemo to do the job of instancing the mesh objects
        // all the user should care about is constructing a new Voro, the user
        // should not have to then manually instantiate the VoroGeometry
        
        if (!_useDebugMesh) {
            // the debug mesh is not in use
            
            // instance all the unique geometry instances for the voro
            // this is very expensive to do, but allows the Voro
            // to resemble how it would in a game
            
            for (var i = 0; i < _voro.Geometry.Length; i++) {
                // instance the mesh objects the voro contains
                InstanceMesh(_voro.Geometry[i].MeshInstance, _voro.Points[i]);
            }
            
            void InstanceMesh(GameObject mesh, Point point) {
                // instantiate a game object as a temporary measure
        
                var instance = Instantiate(mesh, transform, true);
                // offset mesh to correct position
                instance.transform.position += point.position + transform.position;
            }
        }
    }

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
        for (int i = 0; i < numGeos; ++i) {
            // get the location of the point, which is where the geo is created
            // _voro.Points[i].position exists in a local space to the voro
            // {0,0} is the origin of the voro, which is the bottom-left corner
            // the local space extends to {1,1} which is the opposite corner
            // offset the position using the VoroDemo GameObject position
            // this moves the voros local position to the actual location in the game world
            Vector3 pos = _voro.Points[i].position + transform.position;
            instData[i] = Matrix4x4.Translate(pos);
        }
        Graphics.RenderMeshInstanced(rp, _mesh, 0, instData);
    }
}
}
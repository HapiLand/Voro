using DataTypes;
using Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityComponents {

public class VoroDemo : MonoBehaviour {

    public TextAsset JsonFile;
    //string _jsonPath;
    Voro _voro;
    void Start() {
        
        // load json file
        if (JsonFile == null) {
            // load default text asset as one has not been set
            JsonFile = ResourceHelper.LoadResource<TextAsset>("Points/DemoTable");
        }
        //_jsonPath = "Points/Table";
        //var data = LoadResource<TextAsset>(_jsonPath);
        var data = JsonFile;
        
            
        // used to convert the json data
        // the voro has been built using the json text data
        if (!Voro.BuildVoro(data, out _voro)) {
            // unable to build
            return;
        }
        
        // ToDo it might be better for the Voro to automatically ConfigurePointHeight()
        // configure the voro to set its height values
        // transform.position is needed as the voro game objects position in the world
        if (!_voro.ConfigurePointHeight(transform.position)) {
            // unable to configure
            return;
        }

        for (var i = 0; i < _voro.Geometry.Length; i++) {
            // instance the mesh objects the voro contains
            InstanceMesh(_voro.Geometry[i].MeshInstance, _voro.Points[i]);
        }
    }

    void InstanceMesh(GameObject mesh, Point point) {
        // instantiate a game object as a temporary measure
        
        var instance = Instantiate(mesh, transform, true);
        // offset mesh to correct position
        instance.transform.position += point.position + transform.position;
    }
    

    
}
}
using DataTypes;
using Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityComponents {

public class VoroDemo : MonoBehaviour {

    Voro _voro;
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
        
        // used to convert the json data
        // the voro has been built using the json text data
        /*if (!Voro.BuildVoro(configName, transform.position, out _voro)) {
            // unable to build
            return;
        }*/
        
        /*
         the user does not have to tell the Voro to configure height
         the point of using a Voro is to generate usable terrain, why would they
         have to create the voro then manually have to tell it to set height
         
        // configure the voro to set its height values
        // transform.position is needed as the voro game objects position in the world
        if (!_voro.ConfigurePointHeight(transform.position)) {
            // unable to configure
            return;
        }*/

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
using DataTypes;
using UnityEngine;

namespace Internal {
// ToDo reimplement TableSampling
// ToDo reimplement MultiChunks + InfiniteGrid so that Voros can be generated around a player radius
// ToDo implement method for adjacent Voros to blend together nicely
public class Voro {
    readonly Cell[] _cells;
    readonly Vector3 _position;
    GameObject[] _cellObjects;
    JsonConfig _config;

    Voro _original;

    // there must be a better way to find the actual world position
    // for any point, without the voro having to be given the position at all
    // there must also be a better way for a Voro to add GameObjects into the world
    // without Transform needing to make sure they gain the correct parent object
    public Voro(string configName, Vector3 origin) {
        // point data in json file is used to build the cells for this voro
        _cells = ResourceHelper.CreateCellArray();

        // read the configuration
        // get the JsonConfig which contains the configuration objects for the voro height
        _config = new JsonConfig(configName);

        _position = origin;

        // configure the height so the voro is built in its finished form
        ConfigurePointHeight();

        // instance the geometry that exists in this voro
        InstanceGeometry();

        OnCreation();
    }

    bool ConfigurePointHeight() {
        // this class uses a configuration json and can read the data inside it
        // this configuration is to alter the height value of all the voro points
        // the config holds a set of values which are used to form an instruction
        // this instruction is designed to manipulate the height value in a way
        // that allows the look of the terrain to be directed by the user
        // multiple instructions can be stored in the configuration file
        var voroHeight = new VoroHeight((_config, _cells), _position, out var heightMap);

        ApplyHeight(heightMap);

        void ApplyHeight(float[] heightValues) {
            for (var i = 0; i < heightValues.Length; i++) {
                // get the position of each point
                var newPos = _cells[i].position;
                // set the height value in the point
                newPos.y = heightValues[i];
                // set the new position of the point, applying the height value
                _cells[i].position = newPos;
            }
        }

        return true;
    }

    void InstanceGeometry() {
        // instance all the unique geometry instances for the voro
        // this is very expensive to do, but allows the Voro
        // to resemble how it would in a game

        _cellObjects = new GameObject[_cells.Length];

        for (var i = 0; i < _cells.Length; i++) {
            ResourceHelper.InstanceGeometry<GameObject>(_cells[i].GetFBX(), out var instance);
            instance.transform.position += _cells[i].position + _position;
            // ToDo set parent for instance
            //instance.transform.SetParent(_position);

            _cellObjects[i] = instance;
        }
    }

    void OnCreation() {
        // ToDo clone the voro for it to be restored if edited during runtime
        // clone this voro to act as a snapshot, allows this current voro to be safely
        // modifiable, and can easily be reset
    }

    // vor the voro to vork in vealtime, the voro vust vupdate
    public void Update() {
        RefreshHeight();
    }

    void RefreshHeight() {
        // reload the config
        _config = new JsonConfig("MyConfig");

        // solve the height for the points
        ConfigurePointHeight();

        // now the points have gained a new position, the actual game objects need to change
        for (var i = 0; i < _cellObjects.Length; i++) {
            _cellObjects[i].transform.position = _cells[i].position + _position;
        }
    }

    // ToDo implement OnConfigured and the ability to make sure all heights are valid
    // OnConfigured() {
    //     // 1) finalize check to ensure the requested configuration is valid
    //     // ie terrain slope+elevation is between a constant range, correcting errors if bad
    // }
    // OnDeletion() {
    //     // 1) invoke to declare this space is now empty, so anything still using it
    //     // has to stop what its doing (the voro exploded and died rip)
    // }
}
}
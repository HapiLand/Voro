using DataTypes;
using UnityEngine;

namespace Internal {
// ToDo implement TableSample
// ToDo implement MultiChunk
// ToDo adjacency
public class Voro {
    readonly Cell[] _cells;
    readonly Vector2 _origin;
    GameObject[] _cellObjects;

    public Voro(Vector2 origin) {
        _origin = origin;

        // construct the cells for this voro
        _cells = ResourceHelper.CreateCellArray();

        // reconstruct the data from the configuration editor
        // this allows the height of the voro to be solved
        var configuration = new JsonConfiguration("MyConfig");

        // set the height value for every cell in the voro
        var voroHeight = new VoroHeight((configuration, _cells), _origin, out var outElevation);
        SolveCellElevation(outElevation);
        // ToDo EditorCompute does this

        OnCreation();
    }

    void SolveCellElevation(float[] elevations) {
        for (var i = 0; i < elevations.Length; i++) {
            // get the position of this cell
            var newPos = _cells[i].position;
            // set the new y value
            newPos.y = elevations[i];
            // set the position of the cell so it gains the calculated elevation
            _cells[i].position = newPos;
        }
    }

    /// <summary>
    ///     gets an array of all the fbx objects from the cells
    /// </summary>
    /// <returns></returns>
    public (Cell, GameObject)[] CreateCellGameObjects() {
        var cellObjects = new (Cell, GameObject)[_cells.Length];
        for (var i = 0; i < _cells.Length; i++) {
            // read the FBX mesh from this cell
            var instance = _cells[i].GetFBX();
            cellObjects[i] = (_cells[i], instance);
        }

        return cellObjects;
    }

    // void InstanceGeometry() {
    //     // instance all the unique geometry instances for the voro
    //     // this is very expensive to do, but allows the Voro
    //     // to resemble how it would in a game
    //
    //     _cellObjects = new GameObject[_cells.Length];
    //
    //     for (var i = 0; i < _cells.Length; i++) {
    //         ResourceHelper.InstanceGeometry<GameObject>(_cells[i].GetFBX(), out var instance);
    //         // instance.transform.position += _cells[i].position + _transform.position;
    //         // instance.transform.SetParent(_transform);
    //
    //         _cellObjects[i] = instance;
    //     }
    // }

    void OnCreation() { }

    // vor the voro to vork in vealtime, the voro vust vupdate
    public void Update() {
        // RefreshHeight();
    }

    public string ToString() {
        return $"Voro @{_origin}";
    }

    // void RefreshHeight() {
    //     // reload the config
    //     _config = new JsonConfig("MyConfig");
    //
    //     // solve the height for the points
    //     SolveCellElevation();
    //
    //     // now the points have gained a new position, the actual game objects need to change
    //     for (var i = 0; i < _cellObjects.Length; i++) {
    //         // _cellObjects[i].transform.position = _cells[i].position + _transform.position;
    //     }
    // }

    // ToDo error detection and correction in generated terrain
    // OnConfigured() {
    //     // 1) finalize check to ensure the requested configuration is valid
    //     // ie terrain slope+elevation is between a constant range, correcting errors if bad
    // }
}
}
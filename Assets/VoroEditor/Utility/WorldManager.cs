using UnityEngine;
using VoroEditor.Source;

namespace VoroEditor.Utility {
/// <summary>
///     for the editor to interact with the game world scene
/// </summary>
[ExecuteAlways]
public class WorldManager : MonoBehaviour {
    GameObjectFactory _gameObjectFactory;
    WorldTile[,] _tiles;
    public static WorldManager Instance { get; private set; }

    // ToDo correctly destroy all children of this on loading

    /// <summary>
    ///     the size of the worlds map
    /// </summary>
    int[] _dimensions => new[] { 5, 1 };

    void Awake() {
        // create as singleton instance
        if (Instance != null) {
            DestroyImmediate(this);
        }

        Instance = this;

        // construct the tiles for the world

        _tiles = new WorldTile[_dimensions[0], _dimensions[1]];
        for (var x = 0; x < _dimensions[0]; x++) {
            for (var z = 0; z < _dimensions[1]; z++) {
                // create a new tile at this position in the world
                _tiles[x, z] = new WorldTile(x, z);
                _tiles[x, z].TileContainer.transform.SetParent(gameObject.transform);
            }
        }
    }

    void Update() {
        // ToDo update the tiles so they are computed through the editor every frame
    }

    public void UpdateWorld() {
        ComputeWorldTiles();
    }

    /// <summary>
    ///     EditorCompute used with this
    /// </summary>
    public void ComputeWorldTiles() {
        for (var x = 0; x < _dimensions[0]; x++) {
            for (var z = 0; z < _dimensions[1]; z++) {
                EditorCompute.Instance.DoCompute(ref _tiles[x, z]);
            }
        }
        // ToDo update position of the game objects after computing
    }
}
}
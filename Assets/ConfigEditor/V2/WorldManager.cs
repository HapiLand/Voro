using Internal;
using UnityEngine;

namespace ConfigEditor.V2 {
/// <summary>
///     for the editor to interact with the game world scene
/// </summary>
[ExecuteAlways]
public class WorldManager : MonoBehaviour {
    WorldTile[,] _tiles;

    /// <summary>
    ///     the size of the worlds map
    /// </summary>
    int[] _dimensions => new[] { 3, 3 };

    public static WorldManager Instance { get; private set; }
    GameObjectFactory _gameObjectFactory;
    
    void Awake() {
        // create as singleton instance
        if (Instance != null) {
            DestroyImmediate(this);
            return;
        }

        Instance = this;

        // construct the tiles for the world
        _tiles = new WorldTile[_dimensions[0], _dimensions[1]];
        for (var x = 0; x < _dimensions[0]; x++) {
            for (var z = 0; z < _dimensions[1]; z++) {
                // create a new tile at this position in the world
                _tiles[x, z] = new WorldTile(x, z);
                // ToDo generate GameObjects for the diagram data in the tile
            }
        }
        
        // create the GameObjects out of the WorldTiles
        _gameObjectFactory = new GameObjectFactory();
        foreach (var tile in _tiles) {
            _gameObjectFactory.CreateFromDiagram(tile.VoroDiagram, out var geo);
            geo.transform.SetParent(gameObject.transform);
        }
        
        
    }

    void Update() {
        // ToDo update the tiles so they are computed through the editor every frame
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;

        for (var x = 0; x < _dimensions[0]; x++) {
            for (var z = 0; z < _dimensions[1]; z++) {
                var tile = _tiles[x, z];

                Gizmos.color = tile.IsVisible ? Color.green : Color.red;
                Gizmos.color = tile.HasInitialised ? Gizmos.color : Color.cornflowerBlue;

                Gizmos.DrawWireSphere(new Vector3(x, 0, z), 0.1f);
            }
        }
    }
}
}
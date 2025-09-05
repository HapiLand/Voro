using Internal;
using Internal.Grids;
using UnityEngine;

namespace UnityComponents {
[RequireComponent(typeof(PlayerLocation))]
public class GameWorld : MonoBehaviour {
    [SerializeField] [Range(0f, 5f)] float _drawDistance;
    Tile[,] _tiles;

    Vector3 _playerPosition {
        get
        {
            if (PlayerLocation.PlayerTransform != null) {
                return PlayerLocation.PlayerTransform.position;
            }

            return Vector3.zero;
        }
    }

    int[] Dimensions => WorldGrid.Dimensions;

    void Awake() {
        // create the points for this game world
        _tiles = new Tile[Dimensions[0], Dimensions[1]];

        // create the grid of world grid of tiles
        for (var x = 0; x < Dimensions[0]; x++) {
            for (var z = 0; z < Dimensions[1]; z++) {
                // create a new tile at this position in the world
                _tiles[x, z] = new Tile(WorldGrid.PositionAt(x, z));
            }
        }
    }

    void Update() {
        for (var x = 0; x < Dimensions[0]; x++) {
            for (var z = 0; z < Dimensions[1]; z++) {
                // determine if the tile is visible
                var newConstructedVoro = _tiles[x, z].UpdateVisibility(_playerPosition, _drawDistance);

                // when the Tile first becomes visible, it returns the newly constructed Voro
                if (newConstructedVoro != null) {
                    // instantiate the GameObjects for this Voro
                    // so that the Cell Geometry is a child of the Tile object
                    InstanceNewVoro(newConstructedVoro, x, z);
                }

                var tileVisible = _tiles[x, z].IsVisible;
                var tileInitialised = _tiles[x, z].HasInitialised;

                // the voro does exist, and the tile is visible
                if (tileInitialised && tileVisible) {
                    // set the game object as active, they might have been invisible last frame
                    _tiles[x, z].VoroContainer.SetActive(true);
                    _tiles[x, z].VoroInstance.Update();
                }

                // the voro does exist, but the tile is no longer visible
                if (tileInitialised && !tileVisible) {
                    // set as inactive, as the object shouldnt be updated                    
                    _tiles[x, z].VoroContainer.SetActive(false);
                }
            }
        }
    }

    void OnDrawGizmos() {
        if (!Application.isPlaying) {
            return;
        }

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_playerPosition, _drawDistance);

        for (var x = 0; x < Dimensions[0]; x++) {
            for (var z = 0; z < Dimensions[1]; z++) {
                var tile = _tiles[x, z];

                Gizmos.color = tile.IsVisible ? Color.green : Color.red;
                Gizmos.color = tile.HasInitialised ? Gizmos.color : Color.cornflowerBlue;

                Gizmos.DrawWireSphere(tile.CornerPosition, 0.1f);
            }
        }
    }

    void InstanceNewVoro(Voro voroInstance, int x, int z) {
        // the newly constructed voro for this tile
        // Debug.Log($"new Voro {voroInstance.ToString()}");

        var geoContainer = _tiles[x, z].VoroContainer;
        geoContainer.transform.SetParent(gameObject.transform);

        // the cellGeo must set as a child object of a new parent
        // this stops all the objects from crowding the scene hierarchy

        // instantiate the geometry from this voro
        var cellGeo = voroInstance.CreateCellGameObjects();
        foreach (var tuple in cellGeo) {
            var cell = tuple.Item1;
            var geo = tuple.Item2;

            ResourceHelper.InstanceGeometry<GameObject>(geo, out var geoInstance);

            // offset the position of the geometry
            geoInstance.transform.position += _tiles[x, z].CornerPosition;
            geoInstance.transform.position += cell.position;
            geoInstance.transform.SetParent(geoContainer.transform);
        }
    }
}
}
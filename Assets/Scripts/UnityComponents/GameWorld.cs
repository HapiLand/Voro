using Internal;
using Internal.Grids;
using Terrain;
using UnityEngine;

namespace UnityComponents {
[RequireComponent(typeof(PlayerLocation))]
// ToDo the scale of the Voros must be larger, the current size of a 1x1 voro is too small
public class GameWorld : MonoBehaviour {
    // Todo 
    // before the EditorCompute can be used as needed the proof needs to show that
    // it is possible to process the entire Voro at once in VoroHeight
    // refactor the voro usage in GameWorld to output the same result as it currently does
    // just change the way Voro gets used, to match EditorCOmpute
    // Voro Points are processed at once
    // getting GameWorld to use Voro like the V2 Editor needs will make it easy
    // to then move this logic through the editor
    // MyConfig.json to be deprecated as a result of this

    [SerializeField] [Range(0.1f, 5f)] float _drawDistance;
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
                // check whether the tile is visible
                var newDiagram = _tiles[x, z].UpdateVisibility(_playerPosition, _drawDistance);
                // upon the tile becoming visible to the player for the first time, a diagram returns

                if (newDiagram != null) {
                    // the diagram has been computed and contains data for the GameWorld
                    // the contents of the diagram will be used to instantiate the FBX objects
                    // ToDo build Unity Objects from a diagram

                    // instantiate the GameObjects for this Voro
                    // so that the Cell Geometry is a child of the Tile object
                    InstanceVoroDiagram(newDiagram, x, z);
                }

                var tileVisible = _tiles[x, z].IsVisible;
                var tileInitialised = _tiles[x, z].HasInitialised;

                // the voro does exist, and the tile is visible
                if (tileInitialised && tileVisible) {
                    // set the game object as active, they might have been invisible last frame
                    _tiles[x, z].VoroContainer.SetActive(true);
                    // _tiles[x, z].VoroInstance.Update();
                    // ToDo diagram must be updated each frame
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

    void InstanceVoroDiagram(VoroDiagram diagram, int x, int z) {
        // the newly constructed diagram for this tile

        // gameobjects created from the diagram can go into this parent as a container
        var geoContainer = _tiles[x, z].VoroContainer;
        geoContainer.transform.SetParent(gameObject.transform);

        // instantiate the geometry from this diagram
        var factory = new DiagramFactory();
        var diagramGeo = factory.GetDiagramGeometry(diagram);
        
        foreach (var (position, geo) in diagramGeo) {
            ResourceHelper.InstanceGeometry<GameObject>(geo, out var geoInstance);

            // offset the position of the geometry
            geoInstance.transform.position += _tiles[x, z].CornerPosition;
            geoInstance.transform.position += position;
            geoInstance.transform.SetParent(geoContainer.transform);
        }
    }
}
}
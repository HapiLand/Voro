using UnityEngine;
using Voro.Internal.World.GameWorldMap.Areas;
using Voro.Internal.World.GameWorldMap.Primitives;
using Voro.Internal.World.GameWorldMap.WorldTiles;

namespace Voro.Internal.World.GameWorldMap {
[ExecuteAlways]
public class WorldMap : MonoBehaviour {
  #region Serialized Fields
  /// <summary>
  /// The space where the terrain for the world can exist
  /// </summary>
  [SerializeField] PrimitiveObject worldSpace;

  /// <summary>
  /// The area within the primitive where tiles can appear
  /// </summary>
  [SerializeField] AreaGrid worldArea;

  /// <summary>
  /// The tiles in worlds area, chunks are created within each tile
  /// </summary>
  [SerializeField] TileGrid tilemap;
  #endregion

  public static WorldMap Instance { get; private set; }

  #region Event Functions
  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    var parent = gameObject.transform;

    worldSpace = GameObjectUtility.CreateWithComponent<CirclePrim>("Circular Shape", parent);
    // todo infinite line

    worldArea = GameObjectUtility.CreateWithComponent<AreaGrid>("World Area", parent);
    worldArea.SetPrimitive(worldSpace);

    tilemap = GameObjectUtility.CreateWithComponent<TileGrid>("Tile Map", parent);
    tilemap.SetArea(worldArea);
  }
  #endregion
}
}
using UnityEngine;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
[ExecuteAlways]
public class TileEntity : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] Tile tile;
  [SerializeField] MeshComponent meshComponent;

  #endregion


  public Tile Tile => tile;

  #region Event Functions

  void Awake() {
    gameObject.AddComponent<MeshFilter>();
    gameObject.AddComponent<MeshRenderer>();
    gameObject.AddComponent<MaterialComponent>();
    meshComponent = gameObject.AddComponent<MeshComponent>();
  }

  void Update() {
    meshComponent.UpdateHeight();
  }

  #endregion

  public void Initialize(Tile tile, VoroWorld world, VoroMap map) {
    this.tile = tile;
    transform.position = tile.Position.ToVector3();
    meshComponent.Initialize(this, world, map);
  }


}
}
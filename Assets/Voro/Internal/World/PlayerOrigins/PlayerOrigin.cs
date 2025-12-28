using UnityEngine;

namespace Voro.Internal.World.PlayerOrigins {
/// <summary>
/// component to mark where a player is positioned
/// </summary>
[ExecuteAlways]
public class PlayerOrigin : MonoBehaviour {
  #region Event Functions
  void OnEnable() {
    PlayerOriginMonitor.Instance.Register(this);
  }

  void OnDisable() {
    PlayerOriginMonitor.Instance.Unregister(this);
  }
  #endregion
}
}
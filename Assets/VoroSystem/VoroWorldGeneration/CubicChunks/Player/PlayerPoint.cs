using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Player {
/// <summary>
/// the position of the player
/// </summary>
// [ExecuteAlways]
public class PlayerPoint : MonoBehaviour {
  #region Serialized Fields
  [SerializeField] float moveSpeed = 5f;
  [SerializeField] float waypointRadius = 20f;
  [SerializeField] float goalDistance = 2f;
  #endregion

  Vector3 _waypoint;
  static Bounds WaypointBounds => new(new Vector3(10f, 10f, 25f), new Vector3(20f, 20f, 50f));

  #region Event Functions
  void Update() {
    if (Vector3.Distance(transform.position, _waypoint) <= goalDistance) {
      PickNewWaypoint();
    }

    var dt = Application.isPlaying ? Time.deltaTime : 0.02f;
    transform.position = Vector3.MoveTowards(transform.position, _waypoint, moveSpeed * dt);
  }

  void OnEnable() {
    PickNewWaypoint();
  }

  void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Gizmos.DrawLine(transform.position, _waypoint);

    Gizmos.DrawWireCube(WaypointBounds.center, WaypointBounds.size);

    var maxDistance = 5f;
    var faceColor = Color.mediumSeaGreen;
    // DrawFace(CubeFace.Front, maxDistance, faceColor);
    // DrawFace(CubeFace.Back, maxDistance, faceColor);
    // DrawFace(CubeFace.Left, maxDistance, faceColor);
    // DrawFace(CubeFace.Right, maxDistance, faceColor);
    // DrawFace(CubeFace.Top, maxDistance, faceColor);
    // DrawFace(CubeFace.Bottom, maxDistance, faceColor);
    return;

    void DrawFace(CubeFace face, float maxDist, Color faceCol) {
      var faceCenter = WaypointBounds.center;
      var faceSize = WaypointBounds.size;
      switch (face) {
      case CubeFace.Front:
        faceCenter += new Vector3(0, 0, WaypointBounds.size.z / 2f);
        faceSize.z = 0f;
        break;
      case CubeFace.Back:
        faceCenter += new Vector3(0, 0, -WaypointBounds.size.z / 2f);
        faceSize.z = 0f;
        break;
      case CubeFace.Left:
        faceCenter += new Vector3(-WaypointBounds.size.x / 2f, 0, 0);
        faceSize.x = 0f;
        break;
      case CubeFace.Right:
        faceCenter += new Vector3(WaypointBounds.size.x / 2f, 0, 0);
        faceSize.x = 0f;
        break;
      case CubeFace.Top:
        faceCenter += new Vector3(0, WaypointBounds.size.y / 2f, 0);
        faceSize.y = 0f;
        break;
      case CubeFace.Bottom:
        faceCenter += new Vector3(0, -WaypointBounds.size.y / 2f, 0);
        faceSize.y = 0f;
        break;
      }

      var min = faceCenter - faceSize * 0.5f;
      var max = faceCenter + faceSize * 0.5f;
      var closestX = Mathf.Clamp(transform.position.x, min.x, max.x);
      var closestY = Mathf.Clamp(transform.position.y, min.y, max.y);
      var closestZ = Mathf.Clamp(transform.position.z, min.z, max.z);
      var closestPoint = new Vector3(closestX, closestY, closestZ);
      var distance = Vector3.Distance(transform.position, closestPoint) / maxDist;
      distance = Mathf.Clamp01(distance);
      var alpha = Mathf.Lerp(1f, 0f, distance);
      Gizmos.color = new Color(faceCol.r, faceCol.g, faceCol.b, alpha);
      Gizmos.DrawCube(faceCenter, faceSize);
    }
  }
  #endregion

  enum CubeFace {
    Front,
    Back,
    Left,
    Right,
    Top,
    Bottom
  }

  void PickNewWaypoint() {
    var min = WaypointBounds.min;
    var max = WaypointBounds.max;
    var x = Random.Range(min.x, max.x);
    var y = Random.Range(min.y, max.y);
    y = 0;
    // todo editor window to toggle whether to disable vertical waypoint
    var z = Random.Range(min.z, max.z);
    _waypoint = new Vector3(x, y, z);
  }
}
}
using UnityEditor;
using UnityEngine;
using Voro.Internal.World.GameWorldMap;

namespace Voro {
/// <summary>
/// <para> voro internal is only a framework to build the actual runtime code </para>
/// <para>
/// my game world is an infinitely long map along a single direction.
/// </para>
/// <para>
/// the world is active around where the player is located.
/// the world is only visible where the camera can see it.
/// </para>
/// <para>
/// the world uses world generation by reading in a scriptable object asset that
/// has a profile for the style of world generation
/// </para>
/// </summary>
[ExecuteAlways]
public class MyGameWorld : MonoBehaviour {
  #region Serialized Fields
  /// <summary>
  /// The space where the map is found
  /// </summary>
  [SerializeField] WorldMap worldMap;
  #endregion

  #region Event Functions
  void Awake() {
    var parent = gameObject.transform;
    worldMap = GameObjectUtility.CreateWithComponent<WorldMap>("Game World Map", parent);
  }
  #endregion

  // todo provide camera and players position to a system that controls
  //  the visibility/enabling of the GridTiles

  // todo provide scriptable object for graph to the system in order
  //  for the WorldChunks to generate the mesh for the terrain

  [MenuItem("GameObject/Voro/My Game World", false, 999)]
  public static void Create() {
    var obj = GameObjectUtility.CreateWithComponent<MyGameWorld>("Voro Terrain Example");
#if UNITY_EDITOR
    Selection.activeGameObject = obj.gameObject;
#endif
  }
}
}
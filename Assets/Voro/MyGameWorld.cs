using UnityEngine;

namespace Voro {
/// <summary>
/// <para>voro internal is only a framework to build the actual runtime code</para>
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
public class MyGameWorld : MonoBehaviour {
    // todo make a horizontal line, across the line are GridTiles
    //  GridTiles behave like the old GridCubes
    //  
    
    // todo provide camera and players position to a system that controls
    //  the visibility/enabling of the GridTiles
    
    // todo provide scriptable object for graph to the system in order
    //  for the WorldChunks to generate the mesh for the terrain
}
}
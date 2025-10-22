using UnityEngine;
using Voro.Core.VoroSystem;
using Voro.Core.World;

namespace Voro.Systems.Landscape {
/*
 * 1) Declare Boundary
 * a rectangular region, location for all things
 * interactive gizmo to set bounds size
 * region contains spacial World plane
 */
public class LandscapeSystem {
    /// <summary> Interactive gizmo which allows the boundary size to be set </summary>
    BoundingRegion _boundary;

    /// <summary> Represents bounding box for the Landscape </summary>
    Environment _environment;

    /// <summary> Spacial lookup within the Environment, stores a Height texture </summary>
    World _worldPlane;

    public LandscapeSystem() {
        Debug.Log("[Landscape System] Creating Environment");
        _boundary = BoundingRegion.CreateInstance();
        _environment = new Environment(VoroUnityComponent.Instance.transform);
        Debug.Log("[Landscape System] Creating World");
        _worldPlane = new World();
    }
}
}
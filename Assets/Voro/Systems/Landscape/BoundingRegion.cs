using System;
using UnityEngine;

namespace Voro.Systems.Landscape {
/// <summary>
/// Rectangle that defines a bounding box
/// </summary>
public class BoundingRegion : IObserver<BoundarySize> {
    BoundaryGizmo _gizmo;
    IDisposable _unsubscriber;
    bool _first = true;
    BoundarySize _last;
    //https://learn.microsoft.com/en-us/dotnet/standard/events/how-to-implement-an-observer
    
    BoundingRegion() {
        _gizmo = BoundaryGizmo.CreateInstance(10f, 10f, Vector2.zero);
    }
    public static BoundingRegion CreateInstance() {
        var region = new BoundingRegion();
        return region;
    }

    public virtual void Subscribe(IObservable<BoundarySize> provider)
    {
        _unsubscriber = provider.Subscribe(this);
    }
    
    public virtual void Unsubscribe()
    {
        _unsubscriber.Dispose();
    }
}
}
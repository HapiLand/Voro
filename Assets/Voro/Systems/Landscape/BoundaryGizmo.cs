using System;
using UnityEngine;

namespace Voro.Systems.Landscape {
/// <summary>
/// Resizable rectangle
/// </summary>
class BoundaryGizmo {
    BoundaryGizmo((Handle a, Handle b) handles) {
        HandleA = handles.a;
        HandleB = handles.b;
        // HandleA.PositionChanged += HandleChanged;
        // HandleB.PositionChanged += HandleChanged;
    }

    float XSize => Mathf.Abs(HandleA.Position.x - HandleB.Position.x);
    float YSize => Mathf.Abs(HandleA.Position.y - HandleB.Position.y);

    public Vector2 Size => new(XSize, YSize);
    public Vector2 Center => (HandleA.Position + HandleB.Position) / 2f;
    public Handle HandleA { get; }
    public Handle HandleB { get; }

    // public IDisposable Subscribe(IObserver<BoundaryGizmo> observer) {
    //     void Callback(BoundaryGizmo g) {
    //         observer.OnNext(g);
    //     }
    //
    //     OnChanged += Callback;
    //
    //     return new Unsubscriber(() => OnChanged -= Callback);
    // }
    //
    // event Action<BoundaryGizmo> OnChanged;
    //
    // void HandleChanged(Handle handle) {
    //     OnChanged?.Invoke(this);
    // }

    public static BoundaryGizmo CreateInstance(float width, float length, Vector2 center) {
        var halfWidth = width / 2;
        var halfLength = length / 2;

        var posA = new Vector2(center.x - halfWidth, center.y - halfLength);
        var posB = new Vector2(center.x + halfWidth, center.y + halfLength);

        var handles = (new Handle(posA), new Handle(posB));
        return new BoundaryGizmo(handles);
    }

    // class Unsubscriber : IDisposable {
    //     readonly Action _unsubscribe;
    //
    //     public Unsubscriber(Action unsubscribe) {
    //         _unsubscribe = unsubscribe;
    //     }
    //
    //     public void Dispose() {
    //         _unsubscribe();
    //     }
    // }
}
}
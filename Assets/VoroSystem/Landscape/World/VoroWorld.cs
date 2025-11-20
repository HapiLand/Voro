using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Landscape.World {
/// <summary>
/// Defines the World and its contents
/// </summary>
public static class VoroWorld {
    public static BBox WorldBounds;

    public static void GetVoroPieces(string name, out List<VoroPiece> pieces) {
        throw new NotImplementedException();
    }

    public static void GetSmartObjects(out List<SmartObject> smartObjects) {
        throw new NotImplementedException();
    }

    public static void CreateBounds(Vector2 center, int width, int length) {
        WorldBounds = new BBox(Vector2.zero, width, length);
    }
}
}
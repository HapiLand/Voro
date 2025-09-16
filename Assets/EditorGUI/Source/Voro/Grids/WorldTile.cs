using UnityEngine;

namespace EditorGUI.Source.Voro.Grids {
public class WorldTile {
    readonly (int x, int z) _origin;

    public WorldTile(int x, int z) {
        _origin = (x, z);

        IsVisible = true;
        HasInitialised = false;

        TileContainer = new GameObject($"WorldTile [{x},{z}]");

        SetVisibleFirstTime();
    }

    public bool HasInitialised { get; private set; }
    public bool IsVisible { get; private set; }

    public GameObject TileContainer { get; private set; }

    void SetVisibleFirstTime() {
        HasInitialised = true;
    }
}
}
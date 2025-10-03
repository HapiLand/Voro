using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Voro.UI.Internal;

namespace VoroSystem {
[ExecuteAlways]
public class WorldController : MonoBehaviour {
    [SerializeField] List<LayerNodePair> contents = new();
    public List<LayerNodePair> Contents => contents;

    void OnEnable() {
#if UNITY_EDITOR
        EditorApplication.delayCall += () => {
            if (this != null) {
                Window.ShowGUI();
            }
        };
#endif
    }

    void Awake() {
        #region World Map

        // set up the world - copy chunk points to the tilemap
        var tileMap = new TileMap();
        tileMap.SetSize(100, 100); // lock map size, produce Tile[,]
        tileMap.LocateCamera(); // update visibility
        var chunk = new Chunk();
        AssetLoader.BeginLoadingAssets(chunk); // routine to load asset library
        tileMap.Blit(chunk); // copy multi chunks to each visible tile position

        #endregion
    }
}
}
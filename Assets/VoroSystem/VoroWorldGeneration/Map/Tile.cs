using System;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.VoroWorldGeneration.HeightSystem;

namespace VoroSystem.VoroWorldGeneration.Map {
[Serializable]
public class Tile {
  #region Serialized Fields
  [field: SerializeField] public int Index { get; private set; }
  [field: SerializeField] public Vector2 Position { get; private set; }
  [field: SerializeField] public TileEntity Entity { get; private set; }
  [field: SerializeField] public bool Visible { get; private set; }
  #endregion

  public Tile(int index, Vector2 position) {
    Position = position;
    Index = index;
  }

  public void CreateEntity(Transform parentTransform) {
    var gameObject = new GameObject($"Tile_{Index}")
    {
      transform =
      {
        parent = parentTransform
      }
    };
    Entity = gameObject.AddComponent<TileEntity>();
    Entity.Initialize(this);
  }

  public void Update() {
    UpdateVisibility();
    Entity.UpdateHeightSystem();
  }

  void UpdateVisibility() {
    var viewportPos = Camera.main.WorldToViewportPoint(Position.ToVector3());
    Visible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
  }

  [Serializable]
  [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
  public class TileEntity : MonoBehaviour {
    TerrainHeightSystem _heightSystem;
    MeshFilter _mf;
    MeshRenderer _mr;

    #region Event Functions
    void OnDrawGizmos() {
      // _heightSystem?.SampleRegion((coords, height) => { Handles.Label(coords, height.ToString("F1")); });
    }
    #endregion

    public void UpdateHeightSystem() {
      var samplerFunc = _heightSystem.SampleRegion(this);
      var displaced = samplerFunc((position, height) => { Debug.Log($"Position {position} @ {height}"); });

      /*
       * shader -> world height model -> tile requests region -> vertex mapping
       *
       * world height is a global heightfield (with multiple layers)
       * tile asks for height sample at world coordinates
       * tile converts world-space samples into vertex displacement
       */
    }


    public void Initialize(Tile tile) {
      transform.position = new Vector3(tile.Position.x, 0f, tile.Position.y);
      gameObject.name = $"Tile_{tile.Position.x}_{tile.Position.y}";

      TileMaterial();
      TileMesh();
      TileHeight();
      return;

      void TileMaterial() {
        _mr = GetComponent<MeshRenderer>();
        _mr.sharedMaterial = new Material(Resources.Load<Material>("ChunkMaterial"))
        {
          mainTexture = Texture2D.whiteTexture
        };
      }

      void TileMesh() {
        _mf = GetComponent<MeshFilter>();
        _mf.sharedMesh = new MeshBuilder()
          .SetSize(WorldGenTileSettings.TileSize)
          .SetResolution(WorldGenTileSettings.MeshResolution)
          .Build();
      }

      void TileHeight() {
        _heightSystem = new TerrainHeightSystem();
        Debug.Log("TerrainHeightSystem created");
      }
    }
  }
}
}
using System;
using UnityEngine;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
[ExecuteAlways]
public class TileEntity : MonoBehaviour {
    [SerializeField] public Tile tile;
    [SerializeField] MeshComponent mesh;

    void Update() {
        mesh.UpdateHeight();
    }

    public void Initialize(Tile tile) {
        this.tile = tile;
        transform.position = tile.Position.ToVector3();
        mesh = new MeshComponent(this);
        OnCreated?.Invoke(this);
    }

    public static event Action<TileEntity> OnCreated;

    public void Remove() {
        if (Application.isPlaying) {
            Destroy(gameObject);
        }
        else {
            DestroyImmediate(gameObject);
        }
    }
}
}
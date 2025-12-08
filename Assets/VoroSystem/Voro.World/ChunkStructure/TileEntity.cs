using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class TileEntity : ITileEntity {
  #region Serialized Fields

  [SerializeField] Vector2 position;
  [SerializeField] GameObject instance;
  [SerializeField] TileMaterial tileMaterial;
  [SerializeField] TileMesh tileMesh;

  #endregion

  

  public TileEntity(Vector2 position) {
    this.position = position;
  }

  #region ITileEntity Members

  public Vector2 Position => position;
  public GameObject Instance => instance;

  public TileMaterial TileMaterial { get; set; }
  public TileMesh TileMesh { get; set; }

  public void CreateInstance(Transform parent, float size, VoroMap map) {
    instance = new GameObject($"({Position.x:F0},{Position.y:F0})");
    Instance.transform.SetParent(parent);
    Instance.transform.position = Position.ToVector3();

    tileMaterial = new TileMaterial(instance);
    tileMesh = new TileMesh(instance, size, map);
  }

  public void UpdateHeight() {
    tileMesh.UpdateHeight();
  }

  #endregion

  public void SetTexture(Texture2D tex) {
    tileMaterial.SetTexture(tex);
  }

  public Texture2D GetTexture() {
    return tileMaterial.GetMaterial().mainTexture as Texture2D;
  }

  public bool HasPointBuffer() {
    return tileMesh.PointBuffer != null;
  }

  /// <summary>
  /// creates a new PointBuffer
  /// </summary>
  public void CreatePointBuffer() {
    var pointsArray = new MeshVertex.PointData[tileMesh.Vertices.Length];
    for (var i = 0; i < pointsArray.Length; i++) {
      pointsArray[i] = new MeshVertex.PointData
      {
        Position = tileMesh.Vertices[i].position
      };
    }
    CreateStructuredBuffer(ref tileMesh.PointBuffer, pointsArray);
  }
  
  /// <summary>
  /// applies the computed data to the points
  /// </summary>
  public void ReadHeightFromPointBuffer() {
    // get data from buffer
    var data = new MeshVertex.PointData[tileMesh.PointBuffer.count];
    tileMesh.PointBuffer.GetData(data);
    
    // write values to vertices
    tileMesh.Apply(data);
  }

  void CreateStructuredBuffer<T>(ref ComputeBuffer buffer, int count) {
    var stride = Marshal.SizeOf(typeof(T));
    var createNewBuffer = buffer == null || !buffer.IsValid() || buffer.count != count || buffer.stride != stride;
    if (!createNewBuffer) {
      return;
    }
    Release(buffer);
    buffer = new ComputeBuffer(count, stride);
  }

  void CreateStructuredBuffer<T>(ref ComputeBuffer buffer, T[] data) {
    CreateStructuredBuffer<T>(ref buffer, data.Length);
    buffer.SetData(data);
  }

  void Release(params ComputeBuffer[] buffers) {
    foreach (var t in buffers) {
      t?.Release();
    }
  }

  public void ReleasePointBuffer() {
    Release(tileMesh.PointBuffer);
  }

  public ComputeBuffer GetPointBuffer() {
    return tileMesh.PointBuffer;
  }


}
}
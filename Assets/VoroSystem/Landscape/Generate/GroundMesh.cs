using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Landscape.Generate {
/// <summary>
/// The Mesh that spans the full size of the Landscape
/// </summary>
public class GroundMesh {
    public GroundMesh(float density) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets the vertex.y position at the point cloud elevation
    /// </summary>
    /// <param name="target">target for the raycast to hit</param>
    /// <returns>MeshGrid with vertical offset</returns>
    public GroundMesh RayVertical(PointCloud target) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Constructs Mesh instances
    /// </summary>
    /// <param name="addMaterial">
    /// if <c>true</c>, paint the mesh
    /// </param>
    /// <returns></returns>
    public List<TileMesh> Fabricate(bool addMaterial) {
        Debug.Log("Building MeshChunks...");
        throw new NotImplementedException();
    }
}
}
using System;
using UnityEngine;

namespace VoroSystem.Landscape.Generate {
public class PointCloud {
    static PCVolume _volume;
    Vector3[] _normals;
    Vector2[] _uvs;
    Vector3[] _vertices;

    PointCloud(PCVolume volume, float density) {
        _volume = volume;
        Fill(density);
    }

    /// <summary>
    /// </summary>
    /// <param name="region">Location where the Point Cloud is generated</param>
    /// <returns></returns>
    public static PointCloud CreateInstance() {
        Debug.Log("Creating PointCloud...");
        // create a flat volume
        var volume = new PCVolume(true);
        // fill interior of volume with regular grid of points
        return new PointCloud(volume, 1f);
    }

    /// <summary>
    /// Fill the PointCloud with points
    /// </summary>
    static void Fill(float density) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// </summary>
    /// <param name="useChunkHeight">
    /// If <c>true</c>, offset p.y using texture within chunk
    /// </param>
    /// <returns></returns>
    public PointCloud SetVerticalOffset(bool useChunkHeight) {
        throw new NotImplementedException();
    }
}
}
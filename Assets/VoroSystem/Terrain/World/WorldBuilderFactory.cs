using System;
using UnityEngine;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.World {
public static class WorldBuilderFactory {
    /*/// <summary>
    ///     generates a mesh to represent the ground
    /// </summary>
    /// <param name="tile"> the tile which has this mesh </param>
    /// <param name="meshData"> vertices </param>
    /// <exception cref="NotImplementedException"> ground mesh not implemented </exception>
    public static void BuildGroundMesh(ITile tile, ImmutableMeshData meshData) {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     places the world object into the scene
    /// </summary>
    public static GameObject PlaceObjectPiece(IWorldObject piece) {
        Debug.Log($"Placing WorldObject '{piece.ID}' at '{piece.WorldPosition.x:F2} x {piece.WorldPosition.y:F2}'");

        var instance = new GameObject($"Cell Piece {piece.ID}")
        {
            transform =
            {
                position = piece.WorldPosition
            }
        };

        // set the mesh asset component for the instance
        var meshFilter = instance.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = piece.MeshPieceAsset;

        // create the material for the instance
        var meshRenderer = instance.AddComponent<MeshRenderer>();
        var originalMat = Resources.Load<Material>("FbxMat");
        var mat = new Material(originalMat);
        meshRenderer.material = mat;

        return instance;
    }*/
}
}
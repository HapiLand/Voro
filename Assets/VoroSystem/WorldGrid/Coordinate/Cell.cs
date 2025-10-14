using UnityEngine;
using VoroSystem.Terrain.World;

namespace VoroSystem.WorldGrid.Coordinate {
/// <summary>
///     represents a mesh piece for a point inside a Chunk
/// </summary>
public class Cell : ICell {
    /*public Cell(float[] position, int id, float[] color) {
        WorldPosition = new Vector3(position[0], 0, position[1]);
        ID = id;
        Color = new Color(color[0], color[1], color[2], 1.0f);
        CellPiece = new WorldObject(WorldPosition, ID);
    }

    public Vector3 WorldPosition { get; }
    public int ID { get; }
    public Color Color { get; }
    public IWorldObject CellPiece { get; }

    public void InstantiateCell(Transform parent) {
        var instance = WorldBuilderFactory.PlaceObjectPiece(CellPiece);

        // set the parent of the instance so it is stored within the Tile
        instance.transform.SetParent(parent, false);

        // set the color of the instances material
        var material = instance.GetComponent<MeshRenderer>().sharedMaterial;
        material.color = Color;
    }

    public override bool Equals(object obj) {
        return obj is Cell other && ID == other.ID;
    }

    public override int GetHashCode() {
        return ID.GetHashCode();
    }*/
}
}
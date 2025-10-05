using System;
using UnityEngine;

namespace VoroSystem.GridSystem.Interface {
/// <summary>
///     mediator pattern for the GridSystem
/// </summary>
public interface IGridSystemMediator {
    void ForEachCell(Action<Cell> action);
    void ForEachTile(Action<Tile> action);
    void Initialize(Vector2Int size);
}
}
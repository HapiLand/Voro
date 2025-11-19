using OldVoroSystem.Generation;
using UnityEngine;

namespace OldVoroSystem.Landscape {
public interface IChunkTile {
  int Index { get; }
  Vector2 Position { get; }
  bool Visible { get; }
  bool Dirty { get; }
  StateType State { get; }
  float Size { get; }
  BaseResult Result { get; }
  void Update();
}
}
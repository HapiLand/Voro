namespace VoroSystem.Voro.World.TileStructure {
/// <summary>
/// implementation for the state of a Tile
/// </summary>
public interface ITileState {
  bool Visible { get; }
  bool Dirty { get; }
  bool Initialised { get; }
  void Init();
  void Update();
}
}
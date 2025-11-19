namespace OldVoroSystem.Landscape {
public interface IChunkMap<T> where T : class, IChunkTile {
  int SizeX { get; }
  int SizeZ { get; }
  T GetTile(int x, int z);
  T GetTile(int index);
  void SetTile(int x, int z, T tile);
  void SetTile(int index, T tile);
  bool InBounds(int x, int z);
}
}
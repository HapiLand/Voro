namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
/// <summary>
/// interface for all states within the cube world state machine
/// </summary>
public interface IWorldState {
  string Name { get; }

  /// <summary> called when state is activated </summary>
  void EnterState(WorldState world);

  /// <summary> called in each async process, after any tile and cube is created </summary>
  void UpdateState(WorldState world);

  /// <summary> called when leaving a state </summary>
  void ExitState(WorldState world);
}
}
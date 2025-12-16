namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// produces height data from a source (graph->shader)
/// =====
/// represents a producer of terrain height data that writes into the central terrain height system.
/// ---
/// implementations generate or modify height values for a given world-space region.
/// (compute shaders, CPU generation, or procedural modifiers).
/// ---
/// this interface is used only during terrain generation or updates.
/// this is never queried directly by tiles or meshes.
/// ---
/// responsibility is to contribute height data to terrain storage
/// it is not used to query height values, it does not store terrain data
/// </summary>
public interface IHeightProvider { }

/// <summary>
/// provides terrain height with random values for height
/// </summary>
public class RandomHeightProvider : IHeightProvider { }
}
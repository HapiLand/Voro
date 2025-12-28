using UnityEngine;
using Voro.Internal.Terrain.Algorithms;

namespace Voro.Internal.Terrain.Effects {
/// <summary>
/// Object provided to system
/// <example> Noise Effect </example>
/// <example> Slope Effect </example>
/// </summary>
public abstract class TerrainEffect : ScriptableObject {
  #region Serialized Fields
  public string title;
  public TerrainAlgorithm opFunction;
  #endregion
}
}
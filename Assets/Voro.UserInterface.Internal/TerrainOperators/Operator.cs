using UnityEngine;
using Voro.UserInterface.Internal.TerrainComputes;

namespace Voro.UserInterface.Internal.TerrainOperators {
/// <summary>
/// Operation that is used to generate terrain
/// <example> Slope </example>
/// <example> Noise </example>
/// </summary>
public class Operator : ScriptableObject {
  public string title;
  public Compute compute;
}
}
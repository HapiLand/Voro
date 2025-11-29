using System;

namespace VoroSystem.Voro.Compute.V2 {
[Serializable]
public enum OperationMode {
  /// <summary>
  /// Forcefully sets the value to this, overwrite existing.
  /// </summary>
  Set,

  /// <summary>
  /// The generated value will be added to the existing value.
  /// </summary>
  Add,

  /// <summary>
  /// The generated value will be subtracted from the existing value.
  /// </summary>
  Subtract,

  /// <summary>
  /// The existing value will be multiplied by the generated value.
  /// </summary>
  Multiply,

  /// <summary>
  /// Output will be minimum of the existing value and the new value.
  /// </summary>
  Min,

  /// <summary>
  /// Output will be maximum of the existing value and the new value.
  /// </summary>
  Max
}
}
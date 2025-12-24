using System;

namespace VoroSystem.VoroWorldGeneration {
public static class WorldGenEditorEvents {
  /// <summary>
  /// Fired whenever any ParameterData drawer changes a value.
  /// </summary>
  public static event Action OnParametersChanged;

  public static void RaiseParametersChanged() {
    OnParametersChanged?.Invoke();
  }
}
}
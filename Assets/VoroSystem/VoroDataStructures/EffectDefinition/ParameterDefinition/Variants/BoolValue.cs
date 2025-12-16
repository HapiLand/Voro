using System;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants {
[Serializable]
public class BoolValue : ParameterValue {
  #region Serialized Fields
  public bool value;
  #endregion
}
}
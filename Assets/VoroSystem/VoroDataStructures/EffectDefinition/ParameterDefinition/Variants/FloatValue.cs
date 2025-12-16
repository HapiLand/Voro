using System;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants {
[Serializable]
public class FloatValue : ParameterValue {
  #region Serialized Fields
  public float value;
  #endregion
}
}
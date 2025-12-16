using System;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Variants {
[Serializable]
public class FloatValue : ParameterValue {
  #region Serialized Fields
  public float value;
  #endregion
}
}
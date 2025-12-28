using System;

namespace Voro.Internal.Terrain.Attributes {
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EffectAttribute : Attribute {
  public readonly Type AlgorithmType;
  public readonly string Description;

  public EffectAttribute(Type algorithmType, string description = "") {
    Description = description;
    AlgorithmType = algorithmType;
  }
}
}
using System;

namespace VoroSystem.VoroDataStructures.EffectDefinition.Core {
[AttributeUsage(AttributeTargets.Class)]
[Serializable]
public class EffectVariantAttribute : Attribute {
  public EffectVariantAttribute(EffectVariants variant) {
    Variant = variant;
  }

  public EffectVariants Variant { get; }
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;

namespace VoroSystem.VoroDataStructures.EffectDefinition {
public static class EffectFactory {
  static readonly Dictionary<EffectVariants, Type> EffectTypes;

  static EffectFactory() {
    EffectTypes = Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(t => t.IsSubclassOf(typeof(EffectData)) && t.GetCustomAttribute<EffectVariantAttribute>() != null)
      .ToDictionary(
        t => t.GetCustomAttribute<EffectVariantAttribute>().Variant,
        t => t
      );
  }

  public static EffectData Create(EffectVariants variant) {
    if (EffectTypes.TryGetValue(variant, out var type)) {
      return (EffectData)Activator.CreateInstance(type);
    }

    throw new ArgumentException($"Unknown EffectVariant: {variant}");
  }
}
}
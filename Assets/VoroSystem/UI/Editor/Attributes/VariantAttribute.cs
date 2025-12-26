#nullable enable
using System;

namespace VoroSystem.UI.Editor.Attributes {
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class VariantAttribute : Attribute {
  public VariantAttribute(Type enumType, object defaultValue) {
    EnumType = enumType;
    DefaultValue = defaultValue;
  }

  public Type EnumType { get; }
  public object DefaultValue { get; }
}
}
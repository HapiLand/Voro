#nullable enable
using System;

namespace VoroSystem.UI.Editor.Attributes {
public class ToggleAttribute : Attribute {
  public readonly bool DefaultValue; // todo reset field to defaults
  public ToggleAttribute(bool defaultValue = false) {
    DefaultValue = defaultValue;
  }
}
}
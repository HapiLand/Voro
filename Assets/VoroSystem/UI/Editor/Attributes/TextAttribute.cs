#nullable enable
using System;

namespace VoroSystem.UI.Editor.Attributes {
public class TextAttribute : Attribute {
  public readonly string DefaultValue; // todo reset field to defaults

  public TextAttribute(string defaultValue = "") {
    DefaultValue = defaultValue;
  }
}
}
#nullable enable
using System;

namespace VoroSystem.UI.Editor.Attributes {
public class SliderAttribute : Attribute {
  public readonly int DefaultValue; // todo reset field to defaults
  public readonly int Maximum;
  public readonly int Minimum;

  public SliderAttribute(int min, int max, int defaultValue = 0) {
    (Minimum, Maximum) = (min, max);
    DefaultValue = defaultValue;
  }
}
}
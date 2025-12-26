#nullable enable
using System;

namespace VoroSystem.UI.Editor.Attributes {
public class CustomEditorOfAttribute : Attribute {
  public readonly Type OfType;

  public CustomEditorOfAttribute(Type ofType) {
    OfType = ofType;
  }
}
}
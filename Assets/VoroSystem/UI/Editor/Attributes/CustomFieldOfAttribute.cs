#nullable enable
using System;

namespace VoroSystem.UI.Editor.Attributes {
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CustomFieldOfAttribute : Attribute {
  public readonly Type OfType;

  public CustomFieldOfAttribute(Type ofType) {
    OfType = ofType;
  }
}
}
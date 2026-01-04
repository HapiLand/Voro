using System;

namespace Voro.Internal.Terrain.Attributes {
public class ParameterOfAttribute : Attribute {
  public readonly object DefaultValue;
  public readonly Type OfType;

  public ParameterOfAttribute(Type ofType, object defaultValue) {
    OfType = ofType;
    DefaultValue = defaultValue;
  }
}
}
using System;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.V2 {
[Serializable]
public enum DataType {
  SliderFloat,

  /// <summary>
  /// Properties are:
  /// "Min" is a float
  /// "Max" is a float
  /// "CurrentValue" is a float
  /// </summary>
  SliderFloat_Log,

  /// <summary>
  /// Properties are:
  /// "Min" is a int
  /// "Max" is a int
  /// "CurrentValue" is a int
  /// </summary>
  SliderInt,

  /// <summary>
  /// Properties are:
  /// "Min" is a int
  /// "Max" is a int
  /// "CurrentValue" is a int
  /// </summary>
  SliderInt_Log,

  Toggle,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a float
  /// </summary>
  InputFloat,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a int
  /// </summary>
  InputInt,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a string
  /// </summary>
  InputText,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a float
  /// </summary>
  Angle,

  /// <summary>
  /// Properties are:
  /// "Label" is a string
  /// "OnClick" is an action event
  /// </summary>
  Button,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a color
  /// </summary>
  Color,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a tuple (float, float)
  /// </summary>
  Position2D,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is a tuple (float, float, float)
  /// </summary>
  Position3D,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is an array of a tuple (int, float, color)
  /// </summary>
  RampColor,

  /// <summary>
  /// Properties are:
  /// "CurrentValue" is an array of a tuple (int, float, float)
  /// </summary>
  RampFloat
}
}
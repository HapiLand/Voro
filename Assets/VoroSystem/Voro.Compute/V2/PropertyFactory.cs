using System;
using UnityEngine;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2 {
public static class PropertyFactory {
  static readonly SerializableDictionary<DataType, Func<SerializableDictionary<string, object>>> map =
    new()
    {
      {
        DataType.SliderFloat, () => new SerializableDictionary<string, object>
        {
          { "Min", 0f },
          { "Max", 1f },
          { "CurrentValue", 0f },
          { "Logarithm", false }
        }
      },
      {
        DataType.SliderFloat_Log, () => new SerializableDictionary<string, object>
        {
          { "Min", 0f },
          { "Max", 1f },
          { "CurrentValue", 0f },
          { "Logarithm", true }
        }
      },
      {
        DataType.SliderInt, () => new SerializableDictionary<string, object>
        {
          { "Min", 0 },
          { "Max", 1 },
          { "CurrentValue", 0 },
          { "Logarithm", false }
        }
      },
      {
        DataType.SliderInt_Log, () => new SerializableDictionary<string, object>
        {
          { "Min", 0 },
          { "Max", 1 },
          { "CurrentValue", 0 },
          { "Logarithm", true }
        }
      },
      {
        DataType.Toggle, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", false }
        }
      },
      {
        DataType.InputFloat, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", 0f }
        }
      },
      {
        DataType.InputInt, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", 0 }
        }
      },
      {
        DataType.InputText, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", "" }
        }
      },
      {
        DataType.Angle, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", 0f }
        }
      },
      {
        DataType.Button, () => new SerializableDictionary<string, object>
        {
          { "Label", "" },
          { "OnClick", null }
        }
      },
      {
        DataType.Color, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", default(Color) }
        }
      },
      {
        DataType.Position2D, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", (0f, 0f) }
        }
      },
      {
        DataType.Position3D, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", (0f, 0f, 0f) }
        }
      },
      {
        DataType.RampColor, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", Array.Empty<(int, float, Color)>() }
        }
      },
      {
        DataType.RampFloat, () => new SerializableDictionary<string, object>
        {
          { "CurrentValue", Array.Empty<(int, float, float)>() }
        }
      }
    };

  public static SerializableDictionary<string, object> Create(DataType type) {
    return map[type]();
  }
}
}
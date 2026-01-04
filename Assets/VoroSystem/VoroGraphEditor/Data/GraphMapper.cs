using System;
using System.Linq;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants;

namespace VoroSystem.VoroGraphEditor.Data {
public static class GraphMapper {
  public static GraphDataDTO ToDataObject(GraphScriptableObject so) {
    return new GraphDataDTO
    {
      name = so.graphName,
      layerDTOList = so.layers.Select(l => new LayerDataDTO
      {
        name = l.layerName,
        effectDTOList = l.effects.Select(fx => new EffectDataDTO
        {
          effectType = fx.effectType,
          operationType = fx.operationType,
          parameterDTOList = fx.parameters.Select(ToParameterDTO).ToList()
        }).ToList()
      }).ToList()
    };
  }

  // ReSharper disable InconsistentNaming
  static ParameterDataDTO ToParameterDTO(ParameterData arg) =>
    // ReSharper restore InconsistentNaming
    new()
    {
      name = arg.parameterName,
      parameterType = arg.parameterType,
      defaultValue = arg.defaultValue
    };

  static ParameterData ToParameterData(ParameterDataDTO arg) {
    return arg.parameterType switch
    {
      ParameterVariants.FloatField => new ParameterData(arg.name, arg.parameterType,
        new FloatValue { value = Convert.ToSingle(arg.defaultValue) }),
      ParameterVariants.Toggle => new ParameterData(arg.name, arg.parameterType,
        new BoolValue { value = Convert.ToBoolean(arg.defaultValue) }),
      _ => throw new ArgumentException($"Unsupported variant type: {arg.parameterType}")
    };
  }


  public static void ApplyToScriptableObject(
    GraphDataDTO dataObject,
    GraphScriptableObject so
  ) {
    so.graphName = dataObject.name;
    so.layers = dataObject.layerDTOList.Select(l => new LayerData
    {
      layerName = l.name,
      effects = l.effectDTOList.Select(ToEffectData).ToList()
    }).ToList();
  }

  static EffectData ToEffectData(EffectDataDTO arg) {
    /*effects = l.effectDTOList.Select(fx => new EffecttData
{
  effectType = fx.effectType,
  operationType = fx.operationType,
  parameters = fx.parameterDTOList.Select(ToParameterData).ToList()
}).ToList()*/

    switch (arg.effectType) {
    case EffectVariants.Slope: {
      var slope = EffectFactory.Create(EffectVariants.Slope);
      slope.operationType = arg.operationType;

      foreach (var paramDto in arg.parameterDTOList) {
        Debug.Log(paramDto);
      }

      //   var param = effect.Parameters.FirstOrDefault(p => p.Name == paramDto.Name);
      //   if (param != null)
      //   {
      //     param.Value = ToValue(paramDto);
      //   }
      // }
      return slope;
    }
    case EffectVariants.Noise: {
      var noise = EffectFactory.Create(EffectVariants.Noise);
      noise.operationType = arg.operationType;
      return noise;
    }
    default:
      throw new ArgumentOutOfRangeException();
    }
  }
}
}
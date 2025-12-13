using System.Linq;

namespace VoroSystem.Voro.GraphEditor.Data {
public static class GraphMapper {
  public static GraphScriptableObject.DTO ToDataObject(GraphScriptableObject so) {
    return new GraphScriptableObject.DTO
    {
      name = so.graphName,
      layers = so.layers.Select(l => new LayerData.DTO
      {
        name = l.layerName,
        effects = l.effects.Select(fx => new EffectData.DTO
        {
          variantType = fx.variantType,
          operation = fx.operation,
          controls = fx.controls.Select(c => new ControlData.DTO
          {
            name = c.controlName,
            variantType = c.variantType,
            defaultValue = c.defaultValue
          }).ToList()
        }).ToList()
      }).ToList()
    };
  }

  public static void ApplyToScriptableObject(
    GraphScriptableObject.DTO dataObject,
    GraphScriptableObject so
  ) {
    so.graphName = dataObject.name;
    so.layers = dataObject.layers.Select(l => new LayerData
    {
      layerName = l.name,
      effects = l.effects.Select(fx => new EffectData
      {
        variantType = fx.variantType,
        operation = fx.operation,
        controls = fx.controls.Select(c => new ControlData
        {
          controlName = c.name,
          variantType = c.variantType,
          defaultValue = c.defaultValue
        }).ToList()
      }).ToList()
    }).ToList();
  }
}
}
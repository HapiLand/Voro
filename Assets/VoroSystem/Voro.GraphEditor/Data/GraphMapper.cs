using System.Linq;

namespace VoroSystem.Voro.GraphEditor.Data {
public static class GraphMapper {
  public static GraphDataObject ToDataObject(GraphScriptableObject so) {
    return new GraphDataObject
    {
      name = so.Name,
      foo = so.Foo.Select(f => new LayerDataObject
      {
        number = f.Number,
        toggle = f.Toggle
      }).ToList()
    };
  }

  public static void ApplyToScriptableObject(
    GraphDataObject dataObject,
    GraphScriptableObject so
  ) {
    so.Name = dataObject.name;
    so.Foo = dataObject.foo.Select(f => new LayerData
    {
      Number = f.number,
      Toggle = f.toggle
    }).ToList();
  }
}
}
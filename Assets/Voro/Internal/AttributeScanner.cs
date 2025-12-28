using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voro.Internal {
public static class AttributeScanner {
  static List<Type> GetClassesOfAttributeType<TAttr>() where TAttr : Attribute {
    var assembly = Assembly.GetExecutingAssembly();
    var typesWithAttribute = assembly.GetTypes()
      .Where(t => t.GetCustomAttribute<TAttr>() != null)
      .ToList();
    return typesWithAttribute;
  }

  public static void GenerateAssets<TAttr>(Action<Type> action) where TAttr : Attribute {
    var types = GetClassesOfAttributeType<TAttr>();
    foreach (var type in types) {
      var attr = type.GetCustomAttribute<TAttr>();
      if (attr == null) {
        continue;
      }

      action(type);
    }
  }
}
}
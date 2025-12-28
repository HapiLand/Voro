using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using Voro.Internal.Terrain.Attributes;

namespace Voro.Internal.Terrain.Algorithms {
[InitializeOnLoad]
public static class AlgorithmAssetGenerator {
  static AlgorithmAssetGenerator() {
    GenerateAll();
  }

  static void GenerateAll() {
    var algorithmTypes = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(a => {
        try {
          return a.GetTypes();
        }
        catch (ReflectionTypeLoadException e) {
          return e.Types.Where(t => t != null);
        }
      })
      .Where(t => t.IsClass 
                  && !t.IsAbstract 
                  && typeof(ScriptableObject).IsAssignableFrom(t) 
                  && t.GetCustomAttribute<AlgorithmAttribute>() != null);

    foreach (var type in algorithmTypes) {
      var asset = ScriptableObject.CreateInstance(type);

      var method = typeof(AssetUtility)
        .GetMethod(nameof(AssetUtility.CreateOrOverwriteAsset), BindingFlags.Public | BindingFlags.Static);

      method?
        .MakeGenericMethod(type)
        .Invoke(null, new object[] {
          AlgorithmAssetPaths.AlgorithmPath,
          asset
        });
    }

    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
  }

}
}
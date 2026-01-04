using System;

namespace Voro.Internal.Terrain.Attributes {
[AttributeUsage(AttributeTargets.Class)]
public class AlgorithmAttribute : Attribute {
  public readonly string KernelName;

  public AlgorithmAttribute(string kernelName = "CSMain") {
    KernelName = kernelName;
  }
}
}
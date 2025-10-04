using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public class Diagram {
    /// <summary>
    ///     each graph contains a collection of effects
    ///     graph will be computed to produce a form of terrain generation
    /// </summary>
    public readonly List<Graph> Graphs;

    public Diagram(List<LayerData> layerContent) {
        Debug.Log("Creating Diagram");
        var sw = new Stopwatch();
        sw.Start();

        Graphs = new List<Graph>();
        foreach (var layer in layerContent) {
            Graphs.Add(new Graph(layer));
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to create Diagram, it contains {Graphs.Count} Graphs");
    }

    public class Graph {
        public List<EffectBase> Effects;
        public string Name;

        public Graph(LayerData layer) {
            Debug.Log("Creating new Graph instance");
            Name = layer.Name;

            Effects = new List<EffectBase>();
            foreach (var node in layer.Nodes) {
                switch (node.Name) {
                case "SetElevation":
                    Effects.Add(new SetElevation(node.Controls));
                    Debug.Log("Added Effect.SetElevation to graph");
                    break;
                }
            }
        }
    }
}

/// <summary>
///     sets height to a constant value, a flat plane
/// </summary>
public class SetElevation : EffectBase {
    readonly float Amount;
    readonly EffectShaderWrapper ShaderWrapper;

    public SetElevation(LayerData.Node.Control[] data) {
        Amount = data[0].Value; // set the data value for the effect
        ShaderWrapper = new EffectShaderWrapper("SetElevation"); // gets the compute shader for the effect
        Debug.Log("Created new SetElevation Effect");
    }

    public override EffectName Name => EffectName.SetElevation;

    public override void Dispatch(ComputeBuffer buffer, int bufferSize) {
        Debug.Log("Setting parameters in SetElevation compute");
        // create the parameters that drive the compute shader
        var parameters = new Dictionary<string, object>
        {
            { "points", buffer }, // compute buffer stores points from the diagram map
            { "point_count", bufferSize }, // number of points in the map
            { "amount", Amount } // the parameter for the function to do its thing todo as cbuffer
        };

        Debug.Log("Dispatching SetElevation");
        var sw = new Stopwatch();
        sw.Start();
        ShaderWrapper.Dispatch(parameters, bufferSize);
        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to dispatch");
    }
}

/// <summary>
///     base class for an effect, which is a terrain generation function
/// </summary>
public abstract class EffectBase {
    public abstract EffectName Name { get; }
    public abstract void Dispatch(ComputeBuffer buffer, int bufferSize);
}

/// <summary>
///     handles loading and dispatching for an effects shader
/// </summary>
public class EffectShaderWrapper {
    public EffectShaderWrapper(string shaderName, int kernel = 0) {
        Debug.Log($"new EffectShaderWrapper {shaderName}");
        CS = LoadShader(shaderName);
        Kernel = kernel;
    }

    public ComputeShader CS { get; }
    public int Kernel { get; }

    ComputeShader LoadShader(string shaderName) {
        return Resources.Load<ComputeShader>($"{shaderName}");
    }

    public void Dispatch(Dictionary<string, object> parameters, int threadCount) {
        foreach (var kv in parameters) {
            // write the parameter to the shader so it can be dispatched
            switch (kv.Value) {
            case float f: CS.SetFloat(Shader.PropertyToID(kv.Key), f); break;
            case int i: CS.SetInt(Shader.PropertyToID(kv.Key), i); break;
            case ComputeBuffer buf: CS.SetBuffer(Kernel, Shader.PropertyToID(kv.Key), buf); break;
            }
        }

        var threadGroupSizes = GetThreadGroupSizes(CS);
        var numGroupsX = Mathf.CeilToInt(threadCount / (float)threadGroupSizes.x);
        var numGroupsY = Mathf.CeilToInt(1 / (float)threadGroupSizes.y);
        var numGroupsZ = Mathf.CeilToInt(1 / (float)threadGroupSizes.z);
        CS.Dispatch(0, numGroupsX, numGroupsY, numGroupsZ);
    }

    static Vector3Int GetThreadGroupSizes(ComputeShader shader, int kernelIndex = 0) {
        shader.GetKernelThreadGroupSizes(kernelIndex, out var x, out var y, out var z);
        return new Vector3Int((int)x, (int)y, (int)z);
    }
}

public enum EffectName {
    SetElevation
}
}
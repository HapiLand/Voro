using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroWorld.Diagrams;
using VoroWorld.Generation.Effects;
using VoroWorld.Generation.Effects.Base;

namespace VoroWorld.Generation {
/// <summary>
///     diagram goes in
///     execute voro compute
///     result comes out
/// </summary>
public class VoroCompute {
    /// <summary>
    ///     called by the WorldManager to recompute the terrain
    /// </summary>
    public void ComputeWorldTerrain(
        Dictionary<string, List<IEffect>> effectDict,
        VoroDiagram[,] diagrams,
        out VoroResult[,] result) {
        Debug.Log("Compute World Terrain");

        // todo execute all layers, instead of only the first
        var kvp = effectDict.First();
        // this layer will be computed
        var layerName = kvp.Key;
        // the effects in this layer are what hold the generation function
        var effects = kvp.Value;

        // convert VoroDiagrams[,] into VoroResult[,] so that the result
        // will have the identical structure to the original diagrams
        // the VoroResult is what the compute shall alter
        var converted = new VoroResult[diagrams.GetLength(0), diagrams.GetLength(1)];
        for (var x = 0; x < diagrams.GetLength(0); x++) {
            for (var z = 0; z < diagrams.GetLength(1); z++) {
                var d = diagrams[x, z];

                // copy the diagram values to the result
                converted[x, z] = new VoroResult();
                converted[x, z].Points = new CellPoint[d.CellPoints.Length];
                for (var i = 0; i < d.CellPoints.Length; i++) {
                    converted[x, z].Points[i] = new CellPoint
                    {
                        // the input position exist in Local Space, with the Origin use to find the World Space position
                        Position = d.CellPoints[i].Position,
                        Origin = d.Tile.Position,

                        ID = d.CellPoints[i].ID,
                        Color = d.Configuration.PointColors[i]
                    };
                    // debug to get the position, it must be in local space
                    // Debug.Log($"To Compute {converted[x, z].Points[i].Position} (converted to world) ({converted[x, z].Points[i].Position.x + d.Tile.Position.x} {converted[x, z].Points[i].Position.z + d.Tile.Position.z})");
                }
            }
        }

        // execute VoroCompute and return its result
        var computeResult = Execute(layerName, effects, converted);
        // computeResult is a 1D array, convert this data into a 2D VoroResult array
        result = Unflatten(computeResult, diagrams.GetLength(0), diagrams.GetLength(1));
    }


    /// <summary>
    ///     terrain generation method
    /// </summary>
    /// <param name="layerName">the name of the layer that is being executed</param>
    /// <param name="effects">the generation functions to compute</param>
    /// <param name="diagrams">this holds point data for the world position to compute the height at</param>
    /// <returns>a flat VoroResult array that contains the computed output</returns>
    VoroResult[] Execute(
        string layerName,
        List<IEffect> effects,
        VoroResult[,] diagrams
    ) {
        // todo compute every effect, not just the first one
        var effectToCompute = effects.First();

        var worldSpaceDiagrams = new VoroResult[diagrams.GetLength(0), diagrams.GetLength(1)];

        // test to show that the control element slider data value is written to the effect
        // and that the data value does correctly apply a new height value to the terrain
        if (effectToCompute is DefaultEffect effect) {
            var dataValue = effect.Data.Height; // todo read the data value from the effect
            var fakeCompute = new Vector3(0f, dataValue, 0f);

            // computing will produce a result with the locations in world space
            // manually do this process here to ensure DiagramManager can rebuild correctly
            for (var x = 0; x < diagrams.GetLength(0); x++) {
                for (var z = 0; z < diagrams.GetLength(1); z++) {
                    var d = diagrams[x, z];
                    // copy the diagram values to the result
                    worldSpaceDiagrams[x, z] = new VoroResult();
                    worldSpaceDiagrams[x, z].Points = new CellPoint[d.Points.Length];
                    for (var i = 0; i < d.Points.Length; i++) {
                        worldSpaceDiagrams[x, z].Points[i] = new CellPoint
                        {
                            // convert local space to world space
                            Position = d.Points[i].Position + d.Points[i].Origin + fakeCompute,
                            Origin = d.Points[i].Origin,
                            ID = d.Points[i].ID,
                            Color = d.Points[i].Color
                        };
                    }
                }
            }
        }

        return Flatten(worldSpaceDiagrams);


        Debug.Log("Get EditorWindow.Diagram[]");
        /*
         * open a thread for each diagram
         * the type of each effect switches to the designated function.Compute
         */


        Debug.Log("initialize Compute");
        /*
         * the compute function of the effect
         * all derive from same type, all behave the same
         */


        Debug.Log("Get Diagram[,].Points -> buffer");
        /*
         * look into typical ways that chunk data can be computed through the gpu
         */


        Debug.Log("Get Diagram[,].Config -> set properties of Compute");
        /*
         * these are parameters for the function to drive the output
         * if needed also set any constant value for the gpu params
         */


        Debug.Log("Dispatch Compute");
        /*
         * solve point height given the configuration + point properties
         */


        Debug.Log("Read Compute result -> VoroResult[]");
        /*
         * on all threads completing their task
         * Diagram has been turned into VoroResult
         */
    }

    /// <summary>
    ///     2D arrays must be flattened to 1D in order to be computed
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T[] Flatten<T>(T[,] source) {
        var rows = source.GetLength(0);
        var cols = source.GetLength(1);
        var result = new T[rows * cols];

        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                result[i * cols + j] = source[i, j];
            }
        }

        return result;
    }

    /// <summary>
    ///     to apply the result array to the Diagrams, the 1D array needs to have the same dimensions
    ///     as the VoroDiagram[,] _map;
    /// </summary>
    /// <param name="source"></param>
    /// <param name="rows"></param>
    /// <param name="cols"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    T[,] Unflatten<T>(T[] source, int rows, int cols) {
        if (source.Length != rows * cols) {
            throw new ArgumentException("array length does not match dimensions");
        }

        var result = new T[rows, cols];

        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                result[i, j] = source[i * cols + j];
            }
        }

        return result;
    }
}
}

/*
 *
 * /// <summary>
///     this method is called before the terrain generation system recomputes the result
///     any time the control UI changes a data value, VoroCompute needs the terrain to
///     show what the new value does to the terrain
/// </summary>
/// <param name="effect"></param>
/// <param name="value"></param>
void OnControlValueChanged(IEffect effect, object value) {
    // Debug.Log($"EffectData value changed in effect {effect.Name} new value = {value}");

    // EditorWindow will get the data from within the UI tabs and constructs a new object from it
    // the object is provided to VoroCompute, the result of the object produces the full terrain

    // turn the content of the editor into a dictionary which VoroCompute needs to generate terrain
    Dictionary<EditorDiagram, List<IEffect>> editorContent = new();

    // create the keys as the Layers found in the editor
    var layerElements = _layersTab.Query<Layer>().ToList();
    foreach (var layerElement in layerElements) {
        // store this layer and get the effect elements inside it
        var layer = layerElement.EditorDiagram;
        editorContent[layer] = new List<IEffect>();
        // store every effect within this layer
        foreach (var effectElement in layer.EffectElements) {
            editorContent[layer].Add(effectElement.Effect);
        }
    }

    // debug the dictionary content
    // LogDictionary(editorContent);

    // OnEditorOutputToCompute?.Invoke(editorContent);

    void LogDictionary(Dictionary<EditorDiagram, List<IEffect>> dict) {
        Debug.Log("EditorWindow constructed the EditorContent dictionary");
        var sb = new StringBuilder();
        sb.AppendLine("EditorContent Dictionary:");
        foreach (var kvp in dict) {
            var layerName = kvp.Key != null ? kvp.Key.Name : "(null)";
            sb.AppendLine($"- Layer: {layerName}");

            if (kvp.Value != null && kvp.Value.Count > 0) {
                foreach (var effect in kvp.Value) {
                    var effectName = effect != null ? effect.Name : "(null)";
                    sb.AppendLine($"   - Effect: {effectName}");
                }
            }
            else {
                sb.AppendLine("   (no effects)");
            }
        }

        Debug.Log(sb.ToString());
    }
}*/

// public static event Action<Dictionary<EditorDiagram, List<IEffect>>> OnEditorOutputToCompute;
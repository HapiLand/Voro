using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.GraphEditor.Effects {
public abstract class BaseEffect : IEffect {
    protected BaseEffect(string name, EffectType type, List<IBaseControl> controls) {
        Name = name;
        Type = type;
        Items = controls;
    }

    public EffectType Type { get; }
    public string Name { get; }

    public virtual void Draw() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField($"Effect: {Name}");

        EditorGUI.indentLevel++;
        // draw each Control, which displays is Field
        ForEach(control => { control.Draw(); });
        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
    }

    public List<IBaseControl> Items { get; }

    public IBaseControl this[int index] => Items[index];

    public void ForEach(Action<IBaseControl> action) {
        Items.ForEach(action);
    }

    public List<IBaseControl> Controls => Items;

    /// <summary>
    ///     dispatch the Shader, take the existing Result and find new elevation for all its Tile contents
    /// </summary>
    /// <param name="tile"> a result that stores data for Tiles, which the Effect will alter </param>
    /// <returns> the accumulated result that has been modified to find new elevation </returns>
    public IResult Dispatch(ITile tile) {
        Debug.Log(
            $"Dispatch Effect '{Name}' to find the result when this Tile '{tile.Coord.x} x {tile.Coord.y}' is computed");

        CallShader();

        void CallShader() {
            Debug.Log("Dispatching Shader");
        }

        return null;
        Debug.Log($"Effect '{Name}' generated a new Result");
        return Result.CreateTileResult(tile);
    }
}
}
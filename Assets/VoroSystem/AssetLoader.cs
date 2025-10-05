using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public static class AssetLoader {
    /// <summary>
    ///     load the library of assets .fbx .json
    /// </summary>
    public static void BeginLoadingMeshAssets() {
        Debug.Log("Loading assets");
        var sw = new Stopwatch();
        sw.Start();

        // todo load assets

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to load assets");
    }

    /// <summary>
    ///     load a point Table json file
    /// </summary>
    /// <param name="i">table variant to load</param>
    /// <param name="text">the text within the file</param>
    public static void LoadTable(int i, out string text) {
        Debug.Log($"Loading Table{i}.json");
        text = Resources.Load<TextAsset>($"Table{i}").text;
    }

    public static void LoadEditorPreset(int i, out string text) {
        Debug.Log($"Loading Preset{i}.json");
        text = Resources.Load<TextAsset>($"Preset{i}").text;
    }

    public static Mesh GetMeshPiece(int vtxID, int variant = 0) {
        return Resources.Load<Mesh>($"Mesh/{vtxID}_{variant}");
    }
}
}
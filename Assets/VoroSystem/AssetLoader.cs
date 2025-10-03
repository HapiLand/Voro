using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public static class AssetLoader {
    /// <summary>
    ///     load the library of assets .fbx .json
    /// </summary>
    /// <param name="chunk">select the mesh files based on the Chunk ID[]</param>
    public static void BeginLoadingAssets(Chunk chunk) {
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
        var sw = new Stopwatch();
        sw.Start();

        text = Resources.Load<TextAsset>($"Table{i}").text;

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to load file");
    }

    public static void ParsePreset(int i, out string text) {
        Debug.Log($"Loading Preset{i}.json");
        var sw = new Stopwatch();
        sw.Start();

        text = Resources.Load<TextAsset>($"Preset{i}").text;

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to load file");
    }
}
}
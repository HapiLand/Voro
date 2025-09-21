using UnityEngine;

namespace VoroWorld.Generation {
/// <summary>
///     diagram goes in
///     execute voro compute
///     result comes out
/// </summary>
public class VoroCompute {
    public void Execute() {
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
        Debug.Log("ResultsFactory.Build -> VoroResult[] into MeshFilter");
        /*
         * load the mesh assets and construct the desired mesh result
         * is provided to the mesh factory
         */
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
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
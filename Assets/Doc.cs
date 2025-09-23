public class Doc {
    /*
  ================================================================================
                              Voro Terrain System
  ================================================================================

  VoroChunk
  ---------
  - Base blueprint for terrain generation.
  - Holds the point table and configuration that define the base terrain form.
  - User-set effects are applied onto the point data in the chunk point table.

  Chunk
  -----
  - Represents the cell point array and configuration that is parsed from json.

  VoroWorld
  ---------
  - The environment container where generated terrain is placed.

  --------------------------------------------------------------------------------

  VoroUI
  ------
  - User interface layer.
  - This is what the user interacts with.

  LayerDictionary
  ---------------
  - Decides which effects shall be applied in what order.

  TileMap
  -------
  - Stores positional data for all Chunks.
  - Serves as the environment map that VoroWorld will use.

  Diagram
  -------
  - Core structure storing terrain layout.
  - Receives cell point array from Chunk.
  - Receives dictionary from Editor.
  - Outputs positional+mesh data for all Chunks.


  --------------------------------------------------------------------------------

  VoroGeneration
  --------------
  - Oversees the entire terrain generation process.
  - Acts as the central control class of the system.

  VoroCompute
  -----------
  - Executes terrain generation.
  - Generates the actual results based on Diagram instructions.

  VoroResultBuilder
  ---------------
  - Converts results from VoroCompute into 3D environments.
  - Produces instantiated GameObjects and mesh data.

  To Unity Scene
  --------------
  - Final stage where mesh and terrain data are inserted into Unity’s scene graph.
  - Terrain becomes visible and interactive in the 3D environment.

  ================================================================================
  */
}
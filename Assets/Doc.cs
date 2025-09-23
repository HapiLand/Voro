public class Doc {
    /*
  ================================================================================
                              Voro Terrain System
  ================================================================================

  VoroWorld
  ---------
  - The environment container where generated terrain is instantiated.

  TileMap
  -------
  - Stores positional data for all Chunks.
  - Serves as the environment map to be placed into the VoroWorld

  Chunk
  ---------
  - Base blueprint for terrain generation.
  - Parses the point table and configuration that define the base terrain form.
  - User-set effects are applied onto the point data in the chunk.

  VoroUI
  ------
  - This is what the user interacts with.
  - Produces instructions for terrain generation

  Diagram
  -------
  - Core structure storing terrain layout.
  - Receives cell point array from Chunk.
  - Receives dictionary from Editor.
  - Outputs positional+mesh data for all Tiles.

  VoroGeneration
  --------------
  - Oversees the entire terrain generation process.
  - Acts as the central control class of the system.

  VoroCompute
  -----------
  - Executes terrain generation.
  - Generates the actual results based on Diagram instructions.

  ================================================================================
  */
}
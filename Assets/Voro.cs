using VoroWorld;

public class Voro {
    void Main() {
        // set up the world - copy chunk points to the tilemap
        var tileMap = new TileMap();
        tileMap.SetSize(100, 100); // lock map size, produce Tile[,]
        tileMap.LocateCamera(); // update visibility
        var chunk = new Chunk();
        AssetLoader.BeginLoadingAssets(chunk); // routine to load asset library
        tileMap.Blit(chunk); // copy multi chunks to each visible tile position

        // set up the editor - load the initial preset template
        var editor = new VoroEditor();
        editor.ShowWindow(); // open the editor window
        editor.RunDemoCamera(0.2f); // auto fly around the world, 20% speed
        // loading a preset populates the editor with its layer+node content
        editor.LoadPreset(1); // default preset no.1
        editor.CreateDiagram(out var dg); // dictionary
        
        // compute the default terrain
        var compute = new VoroCompute();
        compute.ComputeDiagramMap(tileMap, dg, out var result); // dispatch
        // the compute data is used to build mesh data
        MeshBuilder.BuildVertices(result, out var vtxInfo); // translate result to vertices then
        MeshBuilder.BuildMesh(vtxInfo, out var meshData);   // translate to mesh data
        
        // the mesh data is used to create GameObjects
        WorldBuilder.GenerateWorldMap(meshData); // instances the geometry where it should be
    }
}
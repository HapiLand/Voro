using System;
using VoroSystem.Designer.Graphs;

namespace VoroSystem {
class VoroGraph {
    readonly Voro _voro;
    public LayerGraph Graph;
    public GraphDesigner GraphDesigner;

    public VoroGraph(Voro voro) {
        _voro = voro;
    }

    /// <summary>
    /// New graph
    /// </summary>
    public void InitGraphs() {
        CreateGraphDesigner();
    }

    /// <summary>
    /// copy the height from the surroundings of the layer
    /// </summary>
    /// <param name="arg">the layer mask to sample around the outside of</param>
    /// <returns>the average elevation around this mask</returns>
    public float CopySurroundingHeight(VoroLayer arg) {
        throw new NotImplementedException();
    }

    void CreateGraphDesigner() {
        GraphDesigner = new GraphDesigner();
    }

    public void DesignGraph() {
        // set global feature
        GraphDesigner.GlobalSlopeAngle(
            _voro.VoroInputValue.InputValues.AngleMedian,
            _voro.VoroInputValue.InputValues.AngleMaximum);

        // create layer for a path
        GraphDesigner.NewLayer("Route")

            // give instruction to make a path
            .AddPathEffect(_voro.VoroInputValue.InputValues.PathIrregularity)

            // write this new layer to the graph
            .ToGraph();

        // create layer for safe zones
        GraphDesigner.NewLayer("Camps")

            // scatter points around the layer
            .AddScatterMask(
                _voro.VoroInputValue.InputValues.FlatRegionDensity,
                _voro.VoroInputValue.InputValues.FlatRegionRelaxIterations,
                _voro.VoroInputValue.InputValues.FlatRegionDiameter)

            // create a new layer from the mask
            .NewLayerFromMask("Pieces")

            // smooth edge falloff to blur the mask
            .MaskBlurRadius(1.4f)

            // inherit height from surroundings
            // set constant height to average value
            .SetConstantHeight(CopySurroundingHeight)

            // write this new layer to the graph
            .ToGraph();

        // create locations where voro mesh pieces are created
        GraphDesigner.GetLayer("Pieces")

            // designate this mask as the area to instantiate within
            .MaskToVoroPieces();

        // create the dictionary that holds this graph
        Graph = GraphDesigner.Build();
    }
}
}
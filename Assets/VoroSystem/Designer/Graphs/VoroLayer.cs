using System;

namespace VoroSystem.Designer.Graphs {
class VoroLayer {
    public float Height { get; private set; }

    public VoroLayer AddPathEffect(float irregularity) {
        throw new NotImplementedException();
    }

    public void ToGraph() {
        throw new NotImplementedException();
    }

    public VoroLayer AddScatterMask(float scatterDensity, int relaxIterations, float regionDiameter) {
        throw new NotImplementedException();
    }

    public VoroLayer NewLayerFromMask(string name) {
        throw new NotImplementedException();
    }

    public VoroLayer MaskBlurRadius(float radius) {
        throw new NotImplementedException();
    }

    public VoroLayer SetConstantHeight(Func<VoroLayer, float> heightFunction) {
        Height = heightFunction(this); // apply function to self
        return this;
    }

    public void MaskToVoroPieces() {
        throw new NotImplementedException();
    }
}
}
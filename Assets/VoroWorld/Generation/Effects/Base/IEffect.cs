using VoroWorld.Diagrams;

namespace VoroWorld.Generation.Effects.Base {
public interface IEffect {
    string Name { get; }
    void Compute(ref VoroDiagram diagram);
}
}
using VoroInternal;

namespace VoroSystem {
class VoroInitializer {
    readonly Voro _voro;

    public VoroInitializer(Voro voro) {
        _voro = voro;
    }

    /// <summary>
    /// Initialize all values.
    /// </summary>
    /// <param name="input">default state</param>
    public void Initialize(InputToSystem input) {
        _voro.VoroInputValue.InputValues = _voro.VoroInputValue.SetDefaults(input);
    }
}
}
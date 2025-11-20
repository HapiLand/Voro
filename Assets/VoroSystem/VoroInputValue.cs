using System;
using VoroInternal;

namespace VoroSystem {
class VoroInputValue {
    public InputToSystem InputValues;

    /// <summary>
    /// Set default values.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public InputToSystem SetDefaults(InputToSystem input) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reset to initial condition.
    /// </summary>
    public void RevertToDefaults() {
        throw new NotImplementedException();
    }
}
}
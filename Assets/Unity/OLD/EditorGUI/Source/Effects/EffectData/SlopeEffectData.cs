using System;
using EditorGUI.Source.Effects.EffectData.Base;

namespace EditorGUI.Source.Effects.EffectData {
[Serializable]
public class SlopeEffectData : IEffectData {
    /// <summary>
    ///     the direction of the slope gradient
    /// </summary>
    public float direction;

    /// <summary>
    ///     scale the value of the slope
    /// </summary>
    public float scale;
}
}
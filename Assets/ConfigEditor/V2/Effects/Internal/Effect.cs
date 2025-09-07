namespace ConfigEditor.V2.Effects.Internal {
/// <summary>
///     abstract generic base class for effects which use a specific type of data
///     the foundation for different effect types that have their own configuration
/// </summary>
/// <typeparam name="TEffectData"></typeparam>
public abstract class Effect<TEffectData> : IEffect {
    protected TEffectData Data;
    public Effect(string name, TEffectData data) {
        EffectName = name;
        Data = data;
    }

    public string EffectName { get; }
    public abstract void Compute();

    /// <summary>
    ///     when viewed by the inspector the data for this effect must be able to change
    /// </summary>
    /// <param name="newData"></param>
    public void UpdateData(TEffectData newData) {
        Data = newData;
    }

    public override string ToString() {
        return $"{nameof(Data)}: {Data}, {nameof(EffectName)}: {EffectName}";
    }
}
}
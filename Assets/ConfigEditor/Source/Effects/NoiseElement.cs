using ConfigEditor.Source.Effects.Base;
using Internal;
using Internal.Configuration;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects {
/// <summary>
///     drives the parameters for a Slope effect
/// </summary>
[UxmlElement]
public partial class NoiseElement : CustomElement {
    Slider _scale;

    // these are what will drive the properties for the IConfig
    Slider _size;

    public NoiseElement() {
        Text = "Noise Effect";
    }

    protected override void OnInitializeControls() {
        _size = new Slider("Size", 0f, 5f);
        _size.value = 0f;
        _size.name = "Slider";
        _size.AddToClassList("element-slider");
        Add(_size);
        Controls.Add(_size);

        _scale = new Slider("Scale", 0f, 1f);
        _scale.value = 0f;
        _scale.name = "Slider";
        _scale.AddToClassList("element-slider");
        Add(_scale);
        Controls.Add(_scale);
    }

    public override IConfiguration ToConfig() {
        var cfg = new SlopeCfg();
        cfg.PropertiesArray = new float[2];
        cfg.PropertiesArray[0] = _scale.value;
        cfg.PropertiesArray[1] = _size.value;
        return cfg;
    }
}
}
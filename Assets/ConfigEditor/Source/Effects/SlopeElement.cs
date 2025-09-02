using ConfigEditor.Source.Effects.Base;
using Internal;
using Internal.Configuration;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects {
/// <summary>
///     drives the parameters for a Slope effect
/// </summary>
[UxmlElement]
public partial class SlopeElement : CustomElement {
    // these are what will drive the properties for the IConfig
    Slider _direction;
    Slider _scale;

    public SlopeElement() {
        Text = "Slope Effect";
    }

    protected override void OnInitializeControls() {
        // add the properties that the slope element controls

        _direction = new Slider("Direction", 0f, 360f);
        _direction.value = 0f;
        _direction.name = "Slider";
        _direction.AddToClassList("element-slider");
        Add(_direction);
        Controls.Add(_direction);

        _scale = new Slider("Scale", 0f, 1f);
        _scale.value = 1f;
        _scale.name = "Slider";
        _scale.AddToClassList("element-slider");
        Add(_scale);
        Controls.Add(_scale);
    }

    public override IConfiguration ToConfig() {
        var cfg = new SlopeCfg();
        cfg.PropertiesArray = new float[2];
        cfg.PropertiesArray[0] = _direction.value;
        cfg.PropertiesArray[1] = _scale.value;
        return cfg;
    }
}
}
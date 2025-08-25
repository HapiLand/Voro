using ConfigEditor.Source.Effects.Base;
using Internal;
using Internal.Configuration;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects {
/// <summary>
///     drives the parameters for a Slope effect
/// </summary>
[UxmlElement]
public partial class TerraceElement : CustomElement {
    Slider _direction; // 41

    Slider _iterations;
    Slider _max;
    Slider _min;
    Slider _stepScale;

    public TerraceElement() {
        Text = "Terrace Effect";
    }

    protected override void OnInitializeControls() {
        // add the properties that the noise element controls

        _iterations = new Slider("Iterations", 1f);
        _iterations.value = 4f;
        _iterations.name = "Slider";
        _iterations.AddToClassList("element-slider");
        Add(_iterations);
        Controls.Add(_iterations);

        _min = new Slider("Min", 0f, 0.1f);
        _min.value = 0.01f;
        _min.name = "Slider";
        _min.AddToClassList("element-slider");
        Add(_min);
        Controls.Add(_min);

        _max = new Slider("Max", 0.0f, 0.4f);
        _max.value = 0.2f;
        _max.name = "Slider";
        _max.AddToClassList("element-slider");
        Add(_max);
        Controls.Add(_max);

        _stepScale = new Slider("Step Scale", 0f, 1f);
        _stepScale.value = 0.163f;
        _stepScale.name = "Slider";
        _stepScale.AddToClassList("element-slider");
        Add(_stepScale);
        Controls.Add(_stepScale);

        _direction = new Slider("Direction", 0f, 360f);
        _direction.value = 41f;
        _direction.name = "Slider";
        _direction.AddToClassList("element-slider");
        Add(_direction);
        Controls.Add(_direction);
    }

    public override IConfig ToConfig() {
        var cfg = new SlopeCfg();
        cfg.ConfigArr = new float[5];
        cfg.ConfigArr[0] = _iterations.value;
        cfg.ConfigArr[1] = _min.value;
        cfg.ConfigArr[2] = _max.value;
        cfg.ConfigArr[3] = _stepScale.value;
        cfg.ConfigArr[4] = _direction.value;
        return cfg;
    }
}
}
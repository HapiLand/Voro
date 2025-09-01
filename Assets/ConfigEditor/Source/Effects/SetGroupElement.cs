using ConfigEditor.Source.Effects.Base;
using Internal;
using Internal.Configuration;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects {
/// <summary>
/// </summary>
[UxmlElement]
public partial class SetGroupElement : CustomElement {
    // these are what will drive the properties for the IConfig
    Slider _groupValue;
    public SetGroupElement() {
        Text = "Set Group Effect";
    }

    protected override void OnInitializeControls() {
        _groupValue = new Slider("GroupValue", 0f, 10f);
        _groupValue.value = 0f;
        _groupValue.name = "Slider";
        _groupValue.AddToClassList("element-slider");
        Add(_groupValue);
        Controls.Add(_groupValue);
    }

    public override IConfig ToConfig() {
        var cfg = new SetGroupCfg();
        cfg.ConfigArr = new float[1];
        cfg.ConfigArr[0] = _groupValue.value;
        return cfg;
    }
}
}
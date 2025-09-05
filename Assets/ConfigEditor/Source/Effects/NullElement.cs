using ConfigEditor.Source.Effects.Base;
using Internal;
using Internal.Configuration;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects {
/// <summary>
/// </summary>
[UxmlElement]
public partial class NullElement : CustomElement {
    // these are what will drive the properties for the IConfig
    public NullElement() {
        Text = "Null Effect";
    }

    protected override void OnInitializeControls() { }

    public override IConfiguration ToConfig() {
        var cfg = new NullCfg();
        cfg.PropertiesArray = new float[0];
        return cfg;
    }
}
}
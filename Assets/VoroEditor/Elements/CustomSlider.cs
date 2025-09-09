using UnityEngine.UIElements;

namespace VoroEditor.Elements {
[UxmlElement]
public partial class CustomSlider : VisualElement {
    #region Attributes
    [UxmlAttribute] public string DisplayName { get; set; }
    [UxmlAttribute] public float DefaultValue { get; set; }
    [UxmlAttribute] public float MinValue { get; set; }
    [UxmlAttribute] public float MaxValue { get; set; }
    [UxmlAttribute] public bool IsLogarithmic { get; set; }
    #endregion

    Label _label;
    FloatField _field;
    Slider _slider;

}
}
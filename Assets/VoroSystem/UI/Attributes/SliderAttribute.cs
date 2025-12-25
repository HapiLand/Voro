#nullable enable
using System;

namespace VoroSystem.UI.Attributes {
public class SliderAttribute : Attribute {
    public readonly float Maximum;
    public readonly float Minimum;

    public SliderAttribute(float minimum = 0, float maximum = 1) {
        (Minimum, Maximum) = (minimum, maximum);
    }
}
}
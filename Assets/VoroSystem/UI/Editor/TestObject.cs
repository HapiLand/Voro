using VoroSystem.UI.Attributes;

namespace VoroSystem.UI.Editor {
public class TestObject {
    [Slider(0, 360)] public int Bar = 50;

    [Slider(0, 100)] public int Foo = 50;

}
}
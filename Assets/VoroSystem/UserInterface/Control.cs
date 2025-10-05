using System;
using VoroSystem.UserInterface.Interface;

namespace VoroSystem.UserInterface {
public class Control : IOrderedItem {
    public string Name;
    public float Value;

    public Control(string name, float value) {
        Name = name;
        Value = value;
    }

    public int Index { get; }

    public void SetIndex(int index) {
        throw new NotImplementedException();
    }

    public void Remove() {
        throw new NotImplementedException();
    }
}
}
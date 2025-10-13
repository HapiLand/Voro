using UnityEditor;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;

namespace VoroSystem.GraphEditor.Effects.Parameters {
public class EffectParameterController<T> : BaseControl<T> {
    public EffectParameterController(string name, ITypedParam<T> parameter) : base(name, parameter) { }

    public override void Draw() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        var newValue = Parameter.Value;

        if (typeof(T) == typeof(float)) {
            var val = (float)(object)Parameter.Value;
            val = EditorGUILayout.FloatField(Parameter.Name, val);
            newValue = (T)(object)val;
        }
        else if (typeof(T) == typeof(int)) {
            var val = (int)(object)Parameter.Value;
            val = EditorGUILayout.IntField(Parameter.Name, val);
            newValue = (T)(object)val;
        }
        else if (typeof(T) == typeof(SliderConfig<float>)) {
            var val = (SliderConfig<float>)(object)Parameter.Value;
            var newVal = EditorGUILayout.Slider(Parameter.Name, val.Value, val.Min, val.Max);
            var updated = new SliderConfig<float>(val.Min, val.Max, newVal);
            newValue = (T)(object)updated;
        }
        else if (typeof(T) == typeof(SliderConfig<int>)) {
            var val = (SliderConfig<int>)(object)Parameter.Value;
            var newVal = EditorGUILayout.IntSlider(Parameter.Name, val.Value, val.Min, val.Max);
            var updated = new SliderConfig<int>(val.Min, val.Max, newVal);
            newValue = (T)(object)updated;
        }
        else if (typeof(T) == typeof(bool)) {
            var val = (bool)(object)Parameter.Value;
            val = EditorGUILayout.Toggle(Parameter.Name, val);
            newValue = (T)(object)val;
        }

        if (!Equals(Parameter.Value, newValue)) {
            Parameter.Value = newValue;
            TriggerParameterChanged();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}
}
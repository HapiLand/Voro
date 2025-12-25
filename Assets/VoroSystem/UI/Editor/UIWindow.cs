using UnityEditor;
using UnityEngine;
using VoroSystem.UI.Reflection;

namespace VoroSystem.UI.Editor {
/// <summary>
/// use this to display the custom fields
/// </summary>
public class UIWindow : EditorWindow {
    EditorMember barMember;
    EditorMember fooMember;
    IntField intField;
    TestObject obj;

    void OnEnable() {
        obj = new TestObject();
        intField = new IntField();
        fooMember = EditorMember.Create(typeof(TestObject).GetField(nameof(TestObject.Foo))!);
        barMember = EditorMember.Create(typeof(TestObject).GetField(nameof(TestObject.Bar))!);
    }

    void OnGUI() {
        const float labelWidth = 150f;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Label", GUILayout.Width(labelWidth));
        var fooResult = intField.ProcessInput(fooMember, obj);
        var barResult = intField.ProcessInput(barMember, obj);
        EditorGUILayout.EndHorizontal();
    }

    [MenuItem("Voro/UI Window")]
    public static void ShowWindow() {
        GetWindow<UIWindow>("Window");
    }
}
}
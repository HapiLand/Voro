using System.IO;
using System.Text;
using ConfigEditor.Source.Effects;
using ConfigEditor.Source.Effects.Base;
using Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.UnityComponents {
/// <summary>
///     creates the GUI editor that the user interacts with
/// </summary>
public class ConfigurationEditor : MonoBehaviour {
    Button _addNoiseBtn;
    Button _addSlopeBtn;
    Button _addTerraceBtn;

    VisualElement _containerElement;

    VisualElement _editorUI;
    Button _exportBtn;

    Voro _voro;

    void Awake() {
        _editorUI = GetComponent<UIDocument>().rootVisualElement;
        _containerElement = _editorUI.Q<VisualElement>("Container");

        _voro = ResourceHelper.CreateVoro(transform);
    }

    void Update() {
        ExportConfigJson();
        _voro.Update();
    }

    void OnEnable() {
        _addSlopeBtn = _editorUI.Q<Button>("AddSlope");
        _addSlopeBtn.clicked += InstanceNewSlopeEffect;

        _addNoiseBtn = _editorUI.Q<Button>("AddNoise");
        _addNoiseBtn.clicked += InstanceNewNoiseEffect;

        _addTerraceBtn = _editorUI.Q<Button>("AddTerrace");
        _addTerraceBtn.clicked += InstanceNewTerraceEffect;

        _exportBtn = _editorUI.Q<Button>("Export");
        _exportBtn.clicked += ExportConfigJson;
    }

    void ExportConfigJson() {
        var sb = new StringBuilder();
        var sw = new StringWriter(sb);

        using (JsonWriter writer = new JsonTextWriter(sw)) {
            writer.Formatting = Formatting.Indented;

            // begin writing the json
            writer.WriteStartObject();

            // each effect element is placed inside of config[]
            writer.WritePropertyName("config");
            writer.WriteStartArray();

            // for each effect, its data will be written to the json
            foreach (var child in _containerElement.Children()) {
                // get the effect type of the child
                CustomElement currentEffect = null;
                var effectName = "";

                // ToDo implement better way to find the current effect type
                if (child.Q()[0] is SlopeElement slope) {
                    currentEffect = slope;
                    effectName = "slope";
                }
                else if (child.Q()[0] is NoiseElement noise) {
                    currentEffect = noise;
                    effectName = "noise";
                }
                else if (child.Q()[0] is TerraceElement terrace) {
                    currentEffect = terrace;
                    effectName = "terrace";
                }

                if (currentEffect == null) {
                    continue;
                }

                // read the configuration of this effect
                var config = currentEffect.ToConfig();

                // write the value of config into the json
                writer.WriteStartObject();

                // set the effect name in the json
                writer.WritePropertyName(effectName);

                // write the config data to the json
                var token = JToken.FromObject(config);
                token.WriteTo(writer);

                // all data for this effect has been written to json
                writer.WriteEndObject();
            }

            // all effects in the editor have been written to json
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        // write the data to MyConfig.json
        var json = sb.ToString();
        var fileName = "MyConfig.json";
        var path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log($"Editor wrote to: {path}");
    }

    void InstanceNewSlopeEffect() {
        var uxml = ResourceHelper.LoadEffectUXML("Slope");
        var template = uxml.Instantiate();
        _containerElement.Add(template);
    }

    void InstanceNewNoiseEffect() {
        var uxml = ResourceHelper.LoadEffectUXML("Noise");
        var template = uxml.Instantiate();
        _containerElement.Add(template);
    }

    void InstanceNewTerraceEffect() {
        var uxml = ResourceHelper.LoadEffectUXML("Terrace");
        var template = uxml.Instantiate();
        _containerElement.Add(template);
    }
}
}
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

    GameObject _voroDemoInstance;
    GameObject _voroPrefab;

    void Awake() {
        _editorUI = GetComponent<UIDocument>().rootVisualElement;
        _containerElement = _editorUI.Q<VisualElement>("Container");

        _voroPrefab = ResourceHelper.LoadResource<GameObject>("Prefabs/Voro Demo");
        _voroDemoInstance = Instantiate(_voroPrefab);
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
                var element = child.Q()[0];

                (CustomElement, string) jsonData = element switch
                {
                    SlopeElement slopeEffect => (slopeEffect, "slope"),
                    NoiseElement noiseEffect => (noiseEffect, "noise"),
                    TerraceElement terraceEffect => (terraceEffect, "terrace"),
                    _ => (null, "")
                };

                var effect = jsonData.Item1;
                var effectName = jsonData.Item2;

                if (effectName == "") {
                    continue;
                }


                // read the configuration of this effect
                var config = effect.ToConfig();

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

        // relaod the voro to use the updated config
        UpdateVoroPreview();
    }

    void UpdateVoroPreview() {
        // destroy the existing VoroDemo to a new instance can replace it
        // ToDo VoroDemo should automatically update itself when a json is changed
        Destroy(_voroDemoInstance);

        var instance = Instantiate(_voroPrefab, transform, true);
        _voroDemoInstance = instance;
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
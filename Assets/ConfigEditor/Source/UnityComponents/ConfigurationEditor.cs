using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConfigEditor.Source.Effects;
using ConfigEditor.Source.Effects.Base;
using ConfigEditor.Source.MenuAsset;
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
    VisualElement _containerElement; // where the effects exist

    VisualElement _editorUI;

    List<CustomElement> _effectElements;
    Button _exportBtn;

    //Button _removeBtn;

    Voro _voro;

    void Awake() {
        _editorUI = GetComponent<UIDocument>().rootVisualElement;
        _containerElement = _editorUI.Q<VisualElement>("Container");
        _effectElements = new List<CustomElement>();
        _voro = ResourceHelper.CreateVoro(transform);
    }

    void Update() {
        ExportConfigJson();
        _voro.Update();
    }

    void OnEnable() {
        _exportBtn = _editorUI.Q<Button>("Export");
        _exportBtn.clicked += ExportConfigJson;

        // _removeBtn = _editorUI.Q<Button>("Remove");
        // _removeBtn.clicked += RemoveSelectedEffect;

        // create the uxml asset menu so that effects can be put into categories
        ConstructAssetMenu();
    }

    void ConstructAssetMenu() {
        // ToDo element must float above everything else in the UI

        // load and instance the uxml for the effect menu
        var effectAsset = ResourceHelper.LoadEffectUXML("EffectMenu");
        var template = effectAsset.Instantiate();

        var effectMenuElement = template.Q<EffectMenuElement>();
        if (effectMenuElement != null) {
            effectMenuElement.OnMenuButtonClicked += HandleEffectSelected;
        }

        // add the asset menu to the UI
        var menuContainer = _editorUI.Q<VisualElement>("Controls");
        menuContainer.Add(template);
    }

    #region Effect Selection

    void OnElementSelected(CustomElement sender, bool newValue) {
        if (!newValue) {
            return;
        }

        foreach (var element in _effectElements) {
            if (element != sender) {
                element.Value = false;
            }
        }
    }

    #endregion

    #region Selection Removal

    void RemoveSelectedEffect() {
        var selectedEffect = _effectElements.FirstOrDefault(e => e.Value);

        if (selectedEffect != null) {
            selectedEffect.RemoveConfigElement();
            selectedEffect.RemoveFromHierarchy();
            _effectElements.Remove(selectedEffect);
        }
    }

    #endregion

    #region Json Export

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
                var element = child.Q<CustomElement>();
                //var element = child.Q()[0];

                if (element == null) {
                    continue;
                }

                (CustomElement, string) jsonData = element switch
                {
                    SlopeElement slopeEffect => (slopeEffect, "slope"),
                    NoiseElement noiseEffect => (noiseEffect, "noise"),
                    TerraceElement terraceEffect => (terraceEffect, "terrace"),
                    _ => (null, "")
                };

                var effect = jsonData.Item1;
                var effectName = jsonData.Item2;

                if (effectName == "" || effect == null) {
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
        // Debug.Log($"Editor wrote to: {path}");
    }

    #endregion

    #region Move Selection

    void OnEffectRequestMove(CustomElement sender, int moveValue) {
        // ToDo fix bug where this doesnt move the elements correctly
        // find current index of the sender
        var currentIndex = -1;
        VisualElement currentElement = null;

        foreach (var child in _containerElement.Children()) {
            var element = child.Q<CustomElement>();
            if (element == sender) {
                currentIndex = _containerElement.IndexOf(child);
                currentElement = element;
                break;
            }
        }

        if (currentIndex == -1) {
            Debug.LogError($"{sender.name} has no index");
            return;
        }

        // the new index for the selected effect
        var newIndex = currentIndex + moveValue;
        newIndex = Mathf.Clamp(newIndex, 0, _containerElement.childCount - 1);

        if (currentIndex == newIndex) {
            return;
        }

        // move in the hierarchy
        if (moveValue > 0) {
            _containerElement.RemoveAt(currentIndex);
            _containerElement.Insert(newIndex, currentElement);
        }
        else {
            _containerElement.RemoveAt(currentIndex);
            _containerElement.Insert(newIndex, currentElement);
        }

        // ensure the list of existing effects is updated
        // otherwise only the visual hierarchy will be updated,
        // but selecting an effect would pick the wrong index
        /*var listIndex = _effectElements.IndexOf(sender);
        if (listIndex >= 0) {
            _effectElements.RemoveAt(listIndex);
            _effectElements.Insert(listIndex + moveValue, sender);
        }*/
    }

    #endregion

    #region Effect Creation

    void HandleEffectSelected(VisualTreeAsset effectAsset) {
        if (effectAsset == null) {
            return;
        }

        var effect = AddEffectElement(effectAsset);
    }

    CustomElement AddEffectElement(VisualTreeAsset effectAsset) {
        // load and instance this effect
        var template = effectAsset.Instantiate();

        // subscribe to the selection of the effect
        // the user clicks the event to select it, this displays its properties
        var customElement = template.Q<CustomElement>();
        if (customElement != null) {
            customElement.Selected += OnElementSelected;

            // store every effect that exists in the editor
            _effectElements.Add(customElement);
        }

        // add the effect element into the editors container for all effects
        _containerElement.Add(template);

        // the effect features 2 buttons to move that element up or down the list
        // register when either button is pressed
        var moveUpBtn = template.Q<Button>("MoveUp");
        var moveDownBtn = template.Q<Button>("MoveDown");
        customElement.MoveClicked += OnEffectRequestMove;
        customElement.MoveClicked += OnEffectRequestMove;


        // move the controls for the effect into the editor properties element
        var controls = customElement.Q<VisualElement>("Controls");
        var properties = _editorUI.Q<VisualElement>("Properties");
        properties.Add(controls);

        // deselect all effects that are in the editor
        foreach (var element in _effectElements) {
            element.Value = false;
        }

        return customElement;
    }

    // ToDo when a move button is clicked, find what effect that belongs to, and the move value

    #endregion
}
}
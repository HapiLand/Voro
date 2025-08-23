using System;
using DataTypes;
using Internal;
using Internal.Configuration;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ConfigEditor {
public class SimpleEditorController : MonoBehaviour {
    
    VisualElement _ui;
    VisualElement _configContainer;
    
    Button _slopeButton;
    Button _noiseButton;
    Button _terraceButton;

    JsonConfig _jsonConfig;
    IConfig[] _configs;
    
    void Awake() {
        _ui = GetComponent<UIDocument>().rootVisualElement;
        _configContainer = _ui.Q<VisualElement>("ConfigContainer");
        
        // parse the json to get its configuration
        _jsonConfig = new JsonConfig(ResourceHelper.LoadResource<TextAsset>("Points/LineTable"));
        _configs = _jsonConfig.Configs;
    }
    
    void OnEnable() {
        
        // the elements for the config container will be created automatically
        
        _slopeButton = _configContainer.Q<Button>("slopeBtn");
        _noiseButton = _configContainer.Q<Button>("noiseBtn");
        _terraceButton = _configContainer.Q<Button>("terraceBtn");
        
        // set the label of each button by reading the config
        // JsonConfig has constructed each of the IConfig structs
        for (var i = 0; i < _configs.Length; i++) {
            // read the current configuration
            IConfig cfg = _configs[i];
            
            if (cfg is SlopeCfg slope) {
                _slopeButton.text = slope.ToString();
            }
            else if (cfg is NoiseCfg noise) {
                _noiseButton.text = noise.ToString();
            }
            else if (cfg is TerraceCfg terrace) {
                _terraceButton.text = terrace.ToString();
            }
        }
    }
}
}
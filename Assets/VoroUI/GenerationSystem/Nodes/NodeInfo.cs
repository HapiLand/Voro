using System;
using Source.Utility;
using VoroWorld.Generation.Effects.Internal;

namespace VoroUI.EditorTabs.Nodes {
public class NodeInfo {
    public bool Active;

    /// <summary>
    ///     holds the Data and Control elements to alter the data
    ///     the controls are visible only when Active=true
    /// </summary>
    public INode DataControl;

    public NodeElement Element;

    public EffectNames Name;

    public NodeInfo(EffectNames name) {
        Name = name; // todo select the EffectName via a menu
        Active = false;

        DataControl = EffectHelper.CreateINodeFromName(name);
        if (DataControl is NodeBase nodeBase) {
            nodeBase.OnUpdated += () => {
                // a control in the Node changed some value in its data
                OnValueChanged?.Invoke();
            };
        }

        Element = new NodeElement(this);
        Element.Clicked += state => {
            Active = state;
            if (state) {
                OnActive?.Invoke();
            }
        };
    }

    public event Action OnActive;
    public event Action OnValueChanged;
}
}
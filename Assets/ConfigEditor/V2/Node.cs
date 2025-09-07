using ConfigEditor.V2.Effects;
using ConfigEditor.V2.Effects.Internal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
public class Node : VisualElement {
    IEffect _effect;
    
    Node(IEffect effect) {
        // ToDo implement logic in Node that will execute an actual function for the nodes effect
        //  Node to be used like the IEffect interface in V1
        _effect = effect;
        Debug.Log($"node created with: {_effect.ToString()}");
    }

    public static Node CreateInstance(string effectName) {
        // ToDo implement each IEffect
        // create the new effect instance
        // data which is the configuration for this effect
        // the inspector will display/alter these values
        var defaultFooData = new FooEffectData
        {
            Foo = 1,
            Bar = 2,
            Pee = 3
        };
        // a new instance of the effect, which was cloned from the dictionary
        // this is what the visual element Node shall use
        IEffect nodeEffect = effectName switch
        {
            "Slope" => new FooEffect(defaultFooData),
            "Noise" => new FooEffect(defaultFooData),
            "Terrace" => new FooEffect(defaultFooData),
            "Null" => new FooEffect(defaultFooData),
            "SetGroup" => new FooEffect(defaultFooData),
            _ => null
        };
        // construct the visual element
        var node = new Node(nodeEffect);
        
        return node;
    }
}
}
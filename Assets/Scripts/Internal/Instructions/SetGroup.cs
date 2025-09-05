using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class SetGroup : IEffect {
    readonly IConfiguration _configuration;

    public SetGroup(IConfiguration configuration) {
        _configuration = configuration;
    }

    public float ComputeEffect(float height, Vector3 worldPos) {
        return 0f;
    }

    public void ComputeEffect(ref Cell cell, Vector3 worldPos) {
        // the group to set this cell as
        var groupValue = _configuration.PropertiesArray[0];
        // ToDo effects are limited that they only store floats, when they need to be able to use other types
        cell.GroupID = (int)groupValue;
    }
}
}
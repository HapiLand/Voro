using UnityEngine;

namespace Usage {
[CreateAssetMenu(fileName = "Configuration", menuName = "Voro/Config/New", order = 0)]
public class VoroConfig : ScriptableObject {
    public string configName;
}
}
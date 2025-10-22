using UnityEngine;
using Voro.VoroSystem.Template;

namespace Voro.Core.VoroSystem {
/// <summary> Controls the various Voro systems which are used to create Terrain in an Environment </summary>
[ExecuteAlways]
public class VoroUnityComponent : MonoBehaviour {
    VoroSystemManager _lifecycle;

    void Awake() {
        _lifecycle = new VoroSystemManager(transform);
        _lifecycle.Run();
    }
}
}
using UnityEngine;
using Voro.Systems;

namespace Voro.Core.VoroSystem {
/// <summary> Controls the various Voro systems which are used to create Terrain in an Environment </summary>
[ExecuteAlways]
public class VoroUnityComponent : MonoBehaviour {
    VoroManager _lifecycle;
    public static VoroUnityComponent Instance { get; private set; }
    
    
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _lifecycle = new VoroManager(transform);
        _lifecycle.Run();
    }
}
}
using UnityEngine;
using VoroSystem.Landscape.WorldBoundarySystem;

namespace VoroSystem.Landscape.WorldGridSystem {
[ExecuteInEditMode]
[RequireComponent(typeof(WorldBoundaryComponent))]
public class WorldGridComponent : MonoBehaviour {
    [Range(0.1f, 1f)] float _gridSize = 1f;
    WorldBoundaryComponent _worldBoundary;
    public static WorldGridComponent Instance { get; private set; }

    public (int xSize, int zSize, float gridSize) Dimensions {
        get
        {
            if (_worldBoundary == null) {
                // try to get instance if not initialized yet (editor may call before Start)
                _worldBoundary = WorldBoundaryComponent.Instance;
                if (_worldBoundary == null) {
                    return (1, 1, _gridSize);
                }
            }
            var x = Mathf.Max(1, Mathf.RoundToInt(_worldBoundary.Size.xSize / _gridSize));
            var z = Mathf.Max(1, Mathf.RoundToInt(_worldBoundary.Size.zSize / _gridSize));
            return (x, z, _gridSize);
        }
    }

    public Vector3 Origin {
        get
        {
            if (_worldBoundary != null) {
                return _worldBoundary.Corner.A;
            }

            _worldBoundary = WorldBoundaryComponent.Instance;
            return _worldBoundary == null ? Vector3.zero : _worldBoundary.Corner.A;
        }
    }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _worldBoundary = WorldBoundaryComponent.Instance;
    }
}
}
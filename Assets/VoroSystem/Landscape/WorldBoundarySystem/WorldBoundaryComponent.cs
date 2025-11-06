using UnityEngine;

namespace VoroSystem.Landscape.WorldBoundarySystem {
[ExecuteInEditMode]
public class WorldBoundaryComponent : MonoBehaviour {
    [SerializeField] [Range(1, 10)] int sizeX = 5;
    [SerializeField] [Range(1, 10)] int sizeZ = 5;
    public static WorldBoundaryComponent Instance { get; private set; }

    public (int xSize, int zSize) Size {
        get
        {
            var a = Corner.A;
            var b = Corner.B;
            var x = Mathf.RoundToInt(Mathf.Abs(b.x - a.x));
            var z = Mathf.RoundToInt(Mathf.Abs(b.z - a.z));
            return (x, z);
        }
    }

    public (Vector3 A, Vector3 B) Corner { get; private set; } =
        (new Vector3(0, 0, 0), new Vector3(0, 0, 0));

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start() {
        SetCorners(new Vector3(0, 0, 0), new Vector3(sizeX, 0, sizeZ));
    }

    public void SetCorners(Vector3 cornerA, Vector3 cornerB) {
        Debug.Log("Setting the corners for the world boundary");
        Corner = (cornerA, cornerB);
    }
}
}
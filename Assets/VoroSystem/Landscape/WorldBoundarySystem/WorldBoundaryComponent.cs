using UnityEngine;

namespace VoroSystem.Landscape.WorldBoundarySystem {
/// <summary>
/// Bounding Box
/// </summary>
[ExecuteAlways]
public class WorldBoundaryComponent : MonoBehaviour {
    #region Serialized Fields

    [SerializeField] int sizeX = 10;
    [SerializeField] int sizeZ = 10;

    #endregion

    public (Vector3 A, Vector3 B) Corner { get; private set; }
    public (int xSize, int zSize) Size => (sizeX, sizeZ);

    #region Event Functions

    void OnEnable() {
        SetCorners(Vector3.zero, new Vector3(sizeX, 0, sizeZ));
    }

    #endregion


    public void SetCorners(Vector3 cornerA, Vector3 cornerB) {
        sizeX = Mathf.RoundToInt(Mathf.Abs(cornerB.x - cornerA.x));
        sizeZ = Mathf.RoundToInt(Mathf.Abs(cornerB.z - cornerA.z));
        Corner = (cornerA, cornerB);
    }
}
}
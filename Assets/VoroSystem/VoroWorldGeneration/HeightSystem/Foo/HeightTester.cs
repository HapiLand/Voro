using UnityEditor;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.HeightSystem.Foo {
/// <summary>
/// orchestrate the system and display height
/// </summary>
public class HeightTester : MonoBehaviour {
  /// <summary>
  /// owns the height values within the world.
  /// finds the height values within a region
  /// </summary>
  HeightSystem _heightSystem;

  /// <summary>
  /// region to find the height values within
  /// </summary>
  Region _region;

  #region Event Functions
  void Awake() {
    _heightSystem = new HeightSystem();
    _region = new Region(new Vector2Int(0, 0), 2);
  }

  void OnDrawGizmos() {
    _heightSystem?.SampleRegion(
      _region,
      (coords, height) => { Handles.Label(coords, height.ToString("F1")); });
  }
  #endregion
}
}


/*_heightSystem?.ForEach((coords, height) => {
  var x = coords.Item1;
  var z = coords.Item2;

  var worldX = x * _heightSystem.StepSize;
  var worldZ = z * _heightSystem.StepSize;
  var pos = new Vector3(worldX, 0, worldZ);

  Gizmos.color = Color.gray3;
  Gizmos.DrawWireSphere(pos, 0.05f);
});*/

/*_heightSystem?.SampleRegion(_region, (coords, height) => {
  var x = coords.Item1;
  var z = coords.Item2;
  var worldX = x * (_heightSystem.StepSize * (1f / _region.Resolution));
  var worldZ = z * (_heightSystem.StepSize * (1f / _region.Resolution));
  var pos = new Vector3(worldX, 0, worldZ);
  Handles.Label(pos, height.ToString("F1"));
});*/


// var sizeX = _sample.GetLength(0);
// var sizeZ = _sample.GetLength(1);
// Gizmos.color = Color.green;
// for (var x = 0; x < sizeX; x++) {
//   for (var z = 0; z < sizeZ; z++) {
//     var worldPos = new Vector3(x, 0, z);
//     Gizmos.DrawWireSphere(worldPos, 0.1f);
//   }
// }


/*if (_worldHeights == null) {
  return;
}

// display the region square
DrawRegionSquare();
void DrawRegionSquare() {
  var origin = new Vector3(_region.Position.x, 0, _region.Position.y);
  var regionSize = new Vector3(_region.Size, 0, _region.Size);
  var halfSize = _region.Size / 2f;
  var centerPosition = origin + new Vector3(halfSize, 0, halfSize);
  Gizmos.DrawWireCube(centerPosition, regionSize);
  Gizmos.DrawWireSphere(origin, 0.1f);
}*/

/*// display a red label that shows the entire set of height values in the world
DrawWorldHeights();
void DrawWorldHeights() {
  var wX = _worldHeights.GetLength(0);
  var wZ = _worldHeights.GetLength(1);
  for (var x = 0; x < wX; x++) {
    for (var z = 0; z < wZ; z++) {
      var worldX = x * WorldResolution;
      var worldZ = z * WorldResolution;
      var worldPosition = new Vector3(worldX, 0, worldZ);
      var height = _worldHeights[x, z];
      //Handles.color = Color.red;
      //Handles.Label(worldPosition, height.ToString("F1"));
    }
  }
}*/

// display a green label for every value in the world, and only for values within the region
/*DrawHeightInsideRegion();
void DrawHeightInsideRegion() {
  var wX = _worldHeights.GetLength(0);
  var wZ = _worldHeights.GetLength(1);
  var rStartX = _region.Position.x;
  var rStartZ = _region.Position.y;
  var rEndX = rStartX + _region.Size;
  var rEndZ = rStartZ + _region.Size;

  for (var x = 0; x < wX; x++) {
    for (var z = 0; z < wZ; z++) {
      if (x < rStartX || x >= rEndX || z < rStartZ || z >= rEndZ) {
        continue;
      }
      var height = _worldHeights[x, z];
      var worldPosition = new Vector3(x * WorldResolution, 0, z * WorldResolution);
      Handles.Label(worldPosition, height.ToString("F1"));

      // var worldX = x * WorldResolution;
      // var worldZ = z * WorldResolution;
      // var worldPosition = new Vector3(worldX, 0, worldZ);
      // var height = _worldHeights[x, z];
      //Handles.color = Color.red;
      //
    }
  }

  /*
  var wX = _worldHeights.GetLength(0);
  var wZ = _worldHeights.GetLength(1);

  // region bounds in world coordinates (indices)
  var regionStartX = _region.Position.x;
  var regionStartZ = _region.Position.y;
  var regionEndX = regionStartX + _region.Size;
  var regionEndZ = regionStartZ + _region.Size;

  for (var x = 0; x < wX; x++) {
    for (var z = 0; z < wZ; z++) {
      // skip positions outside the region
      if (x < regionStartX || x >= regionEndX || z < regionStartZ || z >= regionEndZ) {
        continue;
      }

      var height = _worldHeights[x, z];
      var worldPosition = new Vector3(x * WorldResolution, 0, z * WorldResolution);

      Handles.color = Color.green;
      Handles.Label(worldPosition, height.ToString("F1"));
    }
  }#1#
}*/

/*// draw the square region
var rX = _region.Position.x;
var rZ = _region.Position.y;
var size = _region.Size;
var regionWorldOrigin = new Vector3(rX, 0, rZ); // use bottom-left corner
var regionSize = new Vector3(size, 0, size);
Gizmos.DrawWireCube(regionWorldOrigin + new Vector3(size / 2f, 0, size / 2f), regionSize);

// sample within the world heights the values within the region
_regionHeights = new float[RegionResolution, RegionResolution];
for (var x = 0; x < RegionResolution; x++) {
  for (var z = 0; z < RegionResolution; z++) {
    var worldX = rX + x / (float)(RegionResolution - 1) * size;
    var worldZ = rZ + z / (float)(RegionResolution - 1) * size;
    var ix = Mathf.Clamp(Mathf.FloorToInt(worldX), 0, _worldHeights.GetLength(0) - 1);
    var iz = Mathf.Clamp(Mathf.FloorToInt(worldZ), 0, _worldHeights.GetLength(1) - 1);
    _regionHeights[x, z] = _worldHeights[ix, iz];
  }
}

// display the sampled heights in the region
for (var x = 0; x < RegionResolution; x++) {
  for (var z = 0; z < RegionResolution; z++) {
    // Map array indices to world position inside region
    var worldX = rX + x / (float)(RegionResolution - 1) * size;
    var worldZ = rZ + z / (float)(RegionResolution - 1) * size;
    var pos = new Vector3(worldX, 0, worldZ);
    Handles.Label(pos, _regionHeights[x, z].ToString("F1"));
  }
}*/


/*/// <summary>
/// cropped values in the region
/// </summary>
float[,] _sampledHeights;*/
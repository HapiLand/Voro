using UnityEngine;

namespace VoroSystem.Generation.ComputeSystem {
public class ORiverGenerator : MonoBehaviour {
    // Start is called before the first frame update

    static float ElevationMapScale;
    static int Seed;

    static readonly int BoundNorth = 2000;
    static readonly int BoundSouth = 0;
    static readonly int BoundEast = 2000;
    static readonly int BoundWest = 0;

    static Vector2[] Origins;
    static readonly int RiverOriginCount = 90;

    static readonly int MinRiverLength = 100;
    static readonly int MaxRiverLength = 500;

    public static float[,] RiverMap;

    public static void Generate() {
        Random.InitState(0);
        ElevationMapScale = OChunkGenerator.ElevationMapScale;
        Seed = OChunkGenerator.Seed;
        RiverMap = new float[BoundEast - BoundWest, BoundNorth - BoundSouth];

        // fill origins with random locations in range of bounds
        Origins = new Vector2[RiverOriginCount];
        int ox, oz;
        for (var i = 0; i < Origins.Length; i++) {
            ox = Random.Range(BoundWest, BoundEast);
            oz = Random.Range(BoundSouth, BoundNorth);
            if (Mathf.PerlinNoise((ox - Seed + .01f) / ElevationMapScale, (oz - Seed + .01f) / ElevationMapScale) >=
                .5f) {
                Origins[i] = new Vector2(ox, oz);
            }
            else {
                i--;
            }
        }


        float e, eNorth, eSouth, eEast, eWest;
        for (var i = 0; i < Origins.Length; i++) {
            // set up initial point to sample
            var x = (int)Origins[i].x;
            var z = (int)Origins[i].y;
            var forceX = 0;
            var forceZ = 0;
            var riverLength = Random.Range(MinRiverLength, MaxRiverLength);
            //Debug.Log(x.ToString() + " " + z.ToString());


            // sample length of the river
            for (var m = 0; m < riverLength; m++) {
                if (x >= BoundWest && x < BoundEast && z >= BoundSouth && z < BoundNorth) {
                    FillMap(x, z, 1f);
                }

                // get elevation at point and surrounding elevations
                e = Mathf.PerlinNoise((x - Seed + .01f) / ElevationMapScale, (z - Seed + .01f) / ElevationMapScale);
                eNorth = Mathf.PerlinNoise((x - Seed + .01f) / ElevationMapScale,
                    (z - Seed + .01f + 1f) / ElevationMapScale);
                eSouth = Mathf.PerlinNoise((x - Seed + .01f) / ElevationMapScale,
                    (z - Seed + .01f - 1f) / ElevationMapScale);
                eEast = Mathf.PerlinNoise((x - Seed + .01f + 1f) / ElevationMapScale,
                    (z - Seed + .01f) / ElevationMapScale);
                eWest = Mathf.PerlinNoise((x - Seed + .01f - 1f) / ElevationMapScale,
                    (z - Seed + .01f) / ElevationMapScale);
                var pts = new[] { e, eNorth, eSouth, eEast, eWest };

                var lowestPt = Mathf.Min(pts);
                if (lowestPt == e || e < 0f) {
                    if (x >= BoundWest && x < BoundEast && z >= BoundSouth && z < BoundNorth) {
                        FillMap(x, z, 10f);
                    }

                    break;
                }

                // calculate direction of river from surrounding elevations
                forceX = (int)Mathf.Clamp((eEast / eWest - 1f) * 10000f, -5f, 5f);
                forceZ = (int)Mathf.Clamp((eNorth / eSouth - 1f) * 10000f, -5f, 5f);
                Vector3 fVec = new Vector2(forceX, forceZ);
                forceX += (int)(Mathf.PerlinNoise((x + Seed) / 20f + .01f, (z + Seed) / 20f + .01f) * 15f);
                forceZ += (int)(Mathf.PerlinNoise((z - Seed) / 20f + .01f, (z - Seed) / 20f + .01f) * 15f);

                //Debug.Log(new Vector2(forceX, forceZ).ToString());
                var inverseSlope = forceX / (float)forceZ;
                var xFloat = (float)x;

                // fill in segment of river from slope of forceZ and forceX
                for (var fz = 0; fz < Mathf.Abs(forceZ); fz++) {
                    z += (int)Mathf.Sign(forceZ);
                    xFloat += inverseSlope;
                    x = (int)xFloat;
                    FillMap(x, z, 1f);
                    //m++;
                }
            }
        }

        Debug.Log("RiverGenerator: finished");
    }

    static void FillMap(int x, int z, float value) {
        var radius = (int)(10 * value);

        var baseV = new Vector2(x, z);
        var sampleV = new Vector2();
        for (var i = z - radius; i < z + radius; i++) {
            sampleV.y = i;
            for (var j = x - radius; j < x + radius; j++) {
                if (j >= BoundWest && j < BoundEast && i >= BoundSouth && i < BoundNorth) {
                    sampleV.x = j;
                    RiverMap[j, i] = Mathf.Max(RiverMap[j, i], value * (1f / Vector2.Distance(baseV, sampleV)));
                }
            }
        }
    }
}
}